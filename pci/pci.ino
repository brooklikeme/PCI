#include <PinChangeInt.h>
#include <ServoTimer2.h>  // the servo library

#define BLOCK_PIN 11
#define CLAMP_PIN 10
#define MASTER_DIR_PIN     9       // master module direction
#define MASTER_STP_PIN     8       // master module step signal
#define SLAVE_DIR_PIN      7       // slave module direction
#define SLAVE_STP_PIN      6       // slave module step signal
#define CONTRAST_PIN A1
#define PRESSURE_PIN A2
#define SWITCH_PIN A3

#define MASTER_SETTING_PIN A4 // set tracking color for master module
#define SLAVE_SETTING_PIN A5 // set tracking color for slave module

#define MASTER_MOVE_PIN 5 // get master move pwm pulse
#define SLAVE_MOVE_PIN 4 // get slave move pwm pulse
#define MASTER_SPIN_PIN 13 // get master spin pwm pulse
#define SLAVE_SPIN_PIN 12 // get slave spin pwm pulse


#define MASTER_LIMIT_PIN 2 // master limit switch pin
#define SLAVE_LIMIT_PIN 3 // slave limit switch pin


const int stepperSpeed = 800;
const int stepsPerRev = 3200;
const int distancePerRev = 40;
const int distancePerStep = float(distancePerRev) / float(stepsPerRev);
int minSteps = 40;// stepsPerRev / (distancePerRev * 2);
volatile int master_remain_steps = 0;
volatile int slave_reamain_steps = 0;
volatile int master_move_pwm_value = 0;
volatile int master_spin_pwm_value = 0;
volatile int prev_time = 0;
volatile int slave_move_pwm_value = 0;
volatile int slave_spin_pwm_value = 0;
volatile int master_status = 1; // 0: initialize, 1: tracking
volatile int slave_status = 0;  // 0: initialize, 1: tracking
volatile int master_module_position = 0;
volatile int slave_module_position = 0;
int master_kit_position = 0;
int slave_kit_position = 0;
uint8_t latest_interrupted_pin;
const int forwardThreshold = 2;
const int backwardThreshold = 10;

// servo constants
const int noneServoPulse = 1350; 
const int lowServoPulse = 1550; 
const int midServoPulse = 1600; 
const int highServoPulse = 1750; 

const int slow_rate = 3;
boolean master_toggle = 1;
boolean slave_toggle = 1;
int master_slow_index = 0;
const int track_length = 500; 
const int pullback_steps = 8000;

ServoTimer2 clampServo;
ServoTimer2 blockServo; 

volatile int master_last_direction = 1;
volatile int slave_last_direction = 1;
int master_tracing = 0;
int slave_tracing = 0;
int currTime = 0;
int prevReadTime = 0;
int prevInitTime = 0;
int readInterval = 30000;
int initInterval = 30000;
int pressure = 0;
int contrast = 0;
byte switchStatus = 0;
byte readBuffer[4];
int readBufferIndex = 0;
byte prevSendBuffer[15];
byte currSendBuffer[15];
bool master_limit_triggered = false;
bool slave_limit_triggered = false;
int master_move_value = 0;
int slave_move_value = 0;
int master_spin_value = 0;
int slave_spin_value = 0;

ISR(TIMER1_COMPA_vect){ //timer1 interrupt 1Hz toggles pin 13 (LED)
  if (master_status == 0) {
    if (master_slow_index >= slow_rate) {
      master_slow_index = 0;
    } else {
      master_slow_index ++;
      return;
    }
  } else {
    // test
    return;
  }
  if (master_remain_steps > 0) {
    if (master_toggle){
      digitalWrite(MASTER_STP_PIN, HIGH);
    }
    else{
      digitalWrite(MASTER_STP_PIN, LOW);
      master_remain_steps --; 
      // update master module position
      master_module_position += master_last_direction ? distancePerStep : 0 - distancePerStep;
    }
    master_toggle = !master_toggle;   
  }
}

void rising() {
  latest_interrupted_pin = PCintPort::arduinoPin;
  PCintPort::attachInterrupt(latest_interrupted_pin, &falling, FALLING);
  prev_time = micros();
}
 
void falling() {
  latest_interrupted_pin = PCintPort::arduinoPin;
  PCintPort::attachInterrupt(latest_interrupted_pin, &rising, RISING);
  if (latest_interrupted_pin == MASTER_MOVE_PIN) {
    master_move_pwm_value = micros() - prev_time;
  } else {
    master_spin_pwm_value = micros() - prev_time;
  }
}

void setup(){//将步进电机用到的IO管脚设置成输出

  pinMode(PRESSURE_PIN, INPUT);
  pinMode(CONTRAST_PIN, INPUT);
  pinMode(SWITCH_PIN, INPUT_PULLUP);

  pinMode(MASTER_LIMIT_PIN, INPUT_PULLUP);
  pinMode(SLAVE_LIMIT_PIN, INPUT_PULLUP);
  
  pinMode(MASTER_DIR_PIN, OUTPUT);
  pinMode(MASTER_STP_PIN, OUTPUT);
  pinMode(SLAVE_DIR_PIN, OUTPUT);
  pinMode(SLAVE_STP_PIN, OUTPUT);

  pinMode(BLOCK_PIN, OUTPUT);
  pinMode(CLAMP_PIN, OUTPUT);

  pinMode(MASTER_SETTING_PIN, OUTPUT);
  pinMode(SLAVE_SETTING_PIN, OUTPUT);

  pinMode(MASTER_MOVE_PIN, INPUT); 
  digitalWrite(MASTER_MOVE_PIN, HIGH);
  PCintPort::attachInterrupt(MASTER_MOVE_PIN, &rising, RISING);

  pinMode(MASTER_SPIN_PIN, INPUT);
  digitalWrite(MASTER_SPIN_PIN, HIGH);
  PCintPort::attachInterrupt(MASTER_SPIN_PIN, &rising, RISING);

  cli();//stop interrupts
  
  TCCR1A = 0;// set entire TCCR1A register to 0
  TCCR1B = 0;// same for TCCR1B
  TCNT1  = 0;//initialize counter value to 0
  // set compare match register for 1hz increments
  OCR1A = 16000000.0f / 20000;
  // turn on CTC mode
  TCCR1B |= (1 << WGM12);
  // Set CS12 and CS10 bits for 1024 prescaler
  TCCR1B |= (1 << CS10); // no prescaler
  // enable timer compare interrupt
  TIMSK1 |= (1 << OCIE1A);
  
  sei();//allow interrupts

  blockServo.attach(BLOCK_PIN);
  clampServo.attach(CLAMP_PIN);

  blockServo.write(noneServoPulse);
  clampServo.write(3000 - noneServoPulse);

  // initialize serial communication:
  Serial.begin(115200);


}

void initMaster() {
  if (master_limit_triggered) {
    // wait for pulling back complete and set init status
    if (master_remain_steps <= 0) {
      master_limit_triggered = false;
      master_status = 1;
      master_module_position = 0;
    }
  } else {
    if (digitalRead(MASTER_LIMIT_PIN) == LOW) {
      // pull back
      master_limit_triggered = true;
      master_remain_steps = pullback_steps;
      digitalWrite(MASTER_DIR_PIN, HIGH);
    } else {
      master_remain_steps = minSteps;
      digitalWrite(MASTER_DIR_PIN, LOW);
    }
  }
}

void initSlave() {
  
}

void checkHardLimits() {
  // check master and slave limit switchs
  if (digitalRead(MASTER_LIMIT_PIN) == LOW) {
    master_last_direction = !master_last_direction;
    digitalWrite(MASTER_DIR_PIN, master_last_direction);
    master_remain_steps = pullback_steps;
  }
  if (digitalRead(SLAVE_LIMIT_PIN) == LOW) {
    slave_last_direction = !slave_last_direction;
    digitalWrite(SLAVE_DIR_PIN, slave_last_direction);
    master_remain_steps = pullback_steps;
  }  
}

void trackMaster(int input) { 
  // check soft limits
  if (master_module_position <= 0 || master_module_position >= track_length) {
    master_last_direction = !master_last_direction;
    master_remain_steps = minSteps;
    return;
  }
  if (input < -156) {
    if (master_remain_steps > 0 || !master_tracing) {
      if (master_last_direction == 0) {
          master_last_direction = 1; 
          digitalWrite(MASTER_DIR_PIN, HIGH);     
      }
      if (!master_tracing) {
        master_remain_steps = 3200;  
        master_tracing = 1;         
      }   
    } else {
      master_remain_steps = 0;
    }
  } else if (input > 156){
    if (master_remain_steps > 0 || !master_tracing) {
      if (master_last_direction == 1) {
          master_last_direction = 0;        
          digitalWrite(MASTER_DIR_PIN, LOW);      
      }
      if (!master_tracing) {
        master_remain_steps = 3200;  
        master_tracing = 1;         
      }   
    } else {
      master_remain_steps = 0;
    }
  } else {
    if (master_last_direction == 0) {
      if (input < 0 - backwardThreshold) {
        master_last_direction = 1;
        digitalWrite(MASTER_DIR_PIN, HIGH);
        master_remain_steps = minSteps;
      } else if (input > forwardThreshold) {
        master_remain_steps = minSteps;
      } else {
        master_remain_steps = 0;
      }
    } else {
      if (input > backwardThreshold) {
        master_last_direction = 0;
        digitalWrite(MASTER_DIR_PIN, LOW);        
        master_remain_steps = minSteps;
      } else if (input < 0 - forwardThreshold) {
        master_remain_steps = minSteps;
      } else {
        master_remain_steps = 0;
      }    
    } 
    master_tracing = 0;   
  }    
}

void trackSlave(int input) {
  
}

int readPressure() {
  int val = analogRead(PRESSURE_PIN);
  return val;
  return map(val, 0, 1023, 100, 4000);
}

int readContrast() {
  int val = analogRead(CONTRAST_PIN);
  return map(val, 3, 1000, 0, 12000);
}

byte readSwitchStatus() {
  return digitalRead(SWITCH_PIN) == LOW;
}

void triggerEvent(char type, char param) {
  if (type == 1) {
    // init master tracker
    master_status = 0;
    master_limit_triggered = digitalRead(MASTER_LIMIT_PIN) == LOW;
    // loose module
    triggerEvent(11, 0);
    triggerEvent(12, 0);
  } else if (type == 2) {
    slave_status = 0;
    slave_limit_triggered = digitalRead(SLAVE_LIMIT_PIN) == LOW;
    // init slave tracker
  } else if (type == 11) {
    // trigger clamp
    switch(param) {
      case 0:
        clampServo.write(3000 - noneServoPulse);
        break;
      case 1:
        clampServo.write(3000 - lowServoPulse);
        break;
      case 2:
        clampServo.write(3000 - midServoPulse);
        break;
      case 3:
        clampServo.write(3000 - highServoPulse);
        break;
    }
  } else if (type == 12) {
    // triger block
    switch(param) {
      case 0:
        blockServo.write(noneServoPulse);
        break;
      case 1:
        blockServo.write(lowServoPulse);
        break;
      case 2:
        blockServo.write(midServoPulse);
        break;
      case 3:
        blockServo.write(highServoPulse);
        break;
    }    
  }
}

void loop(){
  // read sensors at a constant speed
  currTime = micros();

  // track visions
  if (master_status == 1) {
    // get vision position
    master_move_value = map(master_move_pwm_value * 1.0 / 100.0, 1, 99, 0, 320) - 160;
    master_spin_value = map(master_spin_pwm_value * 1.0 / 100.0, 1, 99, 0, 360);
    // track master module
    trackMaster(master_move_value);    
  }
  if (slave_status == 1) {
    // get vision position
    slave_move_value = map(slave_move_pwm_value * 1.0 / 100.0, 1, 99, 0, 320) - 160;
    slave_spin_value = map(slave_spin_pwm_value * 1.0 / 100.0, 1, 99, 0, 360);
    // track slave module
    trackSlave(slave_move_value);    
  }

  // init modules
  if (currTime - prevInitTime > readInterval) {
    checkHardLimits();
    if (master_status == 0) {
      // init master module
      initMaster();
    }
    if (slave_status == 0) {
      initSlave();
    }
    prevInitTime = currTime;
    // Serial.print("master move value: ");
    // Serial.print(master_move_value);
    // Serial.print(", master spin value: ");
    // Serial.println(master_spin_value);    
  }
  
  if (currTime - prevReadTime > readInterval) {
    // read pressure();
    pressure = readPressure();
    // read contrast()
    contrast = readContrast();
    // read switch status
    switchStatus = readSwitchStatus();    
    // write serial
    /*
     * 起始符   3 
     * 主跟踪单元位置   2
     * 主跟踪单元角度   2
     * 副跟踪单元位置   2
     * 副跟踪单元角度   2
     * 造影剂剂量   2
     * 加压泵压力   2
     * 脚踏开关状态  1
     * 结束符   1
     */
    int master_position = abs(master_move_value + 500);
    currSendBuffer[0] = '^';
    currSendBuffer[1] = lowByte(masterPos);;
    currSendBuffer[2] = highByte(masterPos);
    currSendBuffer[3] = lowByte(0);
    currSendBuffer[4] = highByte(0);
    currSendBuffer[5] = lowByte(0);
    currSendBuffer[6] = highByte(0);
    currSendBuffer[7] = lowByte(0);
    currSendBuffer[8] = lowByte(0);
    currSendBuffer[9] = lowByte(contrast);
    currSendBuffer[10] = highByte(contrast);
    currSendBuffer[11] = lowByte(pressure);
    currSendBuffer[12] = highByte(pressure);
    currSendBuffer[13] = switchStatus;
    currSendBuffer[14] = '\n';

    if (memcmp(currSendBuffer, prevSendBuffer, 15) != 0) {
      Serial.write(currSendBuffer, 15);
      memcpy(prevSendBuffer, currSendBuffer, 15);
    }
   
    // read serial
    while (Serial.available() > 0) {
      // read the incoming byte:
      char ch = Serial.read();
      if (ch == '^') {
        readBufferIndex = 0; 
      } else if (ch == '\n') {
        // trigger control
        triggerEvent(readBuffer[1], readBuffer[2]);       
      }
      if (readBufferIndex < 3) {
        readBuffer[readBufferIndex] = ch;
      }
      readBufferIndex ++;
    }
    
    // update prev read time
    
    prevReadTime = currTime;    

  }
  
}





