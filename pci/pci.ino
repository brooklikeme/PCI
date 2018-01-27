#include <EnableInterrupt.h>
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
#define MASTER_SPIN_PIN 4 // get master spin pwm pulse
#define SLAVE_MOVE_PIN 13 // get slave move pwm pulse
#define SLAVE_SPIN_PIN 12 // get slave spin pwm pulse


#define MASTER_LIMIT_PIN 2 // master limit switch pin
#define SLAVE_LIMIT_PIN 3 // slave limit switch pin

#define PWM_PINS  4

#define PWM_CH1  0
#define PWM_CH2  1
#define PWM_CH3  2
#define PWM_CH4  3

volatile int prev_times[PWM_PINS];
volatile int pwm_values[PWM_PINS];

const int stepperSpeed = 400;
const int stepsPerRev = 3200;
const int distancePerRev = 80;
const float distancePerStep = float(distancePerRev) / float(stepsPerRev);
const int master_vision_width = 60;
const int slave_vision_width = 60;
int minSteps = 80;// stepsPerRev / (distancePerRev * 2);
volatile int master_remain_steps = 0;
volatile int slave_remain_steps = 0;
volatile int master_move_pwm_value = 0;
volatile int master_spin_pwm_value = 0;
volatile int prev_time = 0;
volatile int slave_move_pwm_value = 0;
volatile int slave_spin_pwm_value = 0;
volatile int master_status = 1; // 0: initializing, 1: tracking
volatile int slave_status = 1;  // 0: initializing, 1: tracking
volatile float master_module_position = 0;
volatile float slave_module_position = 0;
const int trackThreshold = 80;

// servo constants
const int min_servo_pulse = 1200;
const int max_servo_pulse = 1750; 

volatile boolean master_toggle = 1;
volatile boolean slave_toggle = 1;
const int track_length = 750; 
const int pullback_steps = 800;

ServoTimer2 clampServo;
ServoTimer2 blockServo; 

volatile int master_last_direction = 1;
volatile int slave_last_direction = 1;
int master_tracing = 0;
int master_tracing_times = 0;
int slave_tracing = 0;
int slave_tracing_times = 0;
const int tracing_ok_times = 100;
int currTime = 0;
int prevReadTime = 0;
int prevInitTime = 0;
int readInterval = 30000;
int initInterval = 3000;
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

int master_position = 0;
int master_rotation = 0;

ISR(TIMER1_COMPA_vect){
  if (slave_remain_steps > 0) {
    if (slave_toggle){
      digitalWrite(SLAVE_STP_PIN, HIGH);
    }
    else{
      digitalWrite(SLAVE_STP_PIN, LOW);
      slave_remain_steps --;       
      // update slave module position
      slave_module_position += slave_last_direction ? 0 - distancePerStep : distancePerStep;
    }
    slave_toggle = !slave_toggle;   
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

void calc_pwm(int channel, int pwm_pin) {
  if (digitalRead(pwm_pin) == HIGH) {
    prev_times[channel] = micros();
  } else {
    pwm_values[channel] = micros() - prev_times[channel] + 40;
  }
}

void calc_pwm_value1() { calc_pwm(PWM_CH1, MASTER_MOVE_PIN); }
void calc_pwm_value2() { calc_pwm(PWM_CH2, MASTER_SPIN_PIN); }
void calc_pwm_value3() { calc_pwm(PWM_CH3, SLAVE_MOVE_PIN); }
void calc_pwm_value4() { calc_pwm(PWM_CH4, SLAVE_SPIN_PIN); }

/*
void rising() {
  latest_interrupted_pin = PCintPort::arduinoPin;
  PCintPort::attachInterrupt(latest_interrupted_pin, &falling, FALLING);
  prev_time = micros();
}
 
void falling() {
  latest_interrupted_pin = PCintPort::arduinoPin;
  PCintPort::attachInterrupt(latest_interrupted_pin, &rising, RISING);
  if (latest_interrupted_pin == MASTER_MOVE_PIN) {
    master_move_pwm_value = micros() - prev_time + 40;
  } else if (latest_interrupted_pin == MASTER_SPIN_PIN) {
    master_spin_pwm_value = micros() - prev_time + 40;
  } else if (latest_interrupted_pin == SLAVE_MOVE_PIN) {
    slave_move_pwm_value = micros() - prev_time + 40;
  } else if (latest_interrupted_pin == SLAVE_SPIN_PIN) {
    slave_spin_pwm_value = micros() - prev_time + 40;
  }
}*/

void setup(){//将步进电机用到的IO管脚设置成输出

  pinMode(PRESSURE_PIN, INPUT);
  pinMode(CONTRAST_PIN, INPUT);
  pinMode(SWITCH_PIN, INPUT_PULLUP);

  pinMode(MASTER_LIMIT_PIN, INPUT_PULLUP);
  pinMode(SLAVE_LIMIT_PIN, INPUT_PULLUP);
  
  pinMode(MASTER_DIR_PIN, OUTPUT);
  digitalWrite(MASTER_DIR_PIN, HIGH);
  pinMode(MASTER_STP_PIN, OUTPUT);
  pinMode(SLAVE_DIR_PIN, OUTPUT);
  digitalWrite(MASTER_DIR_PIN, HIGH);
  pinMode(SLAVE_STP_PIN, OUTPUT);

  pinMode(BLOCK_PIN, OUTPUT);
  pinMode(CLAMP_PIN, OUTPUT);

  pinMode(MASTER_SETTING_PIN, OUTPUT);
  pinMode(SLAVE_SETTING_PIN, OUTPUT);

  pinMode(MASTER_MOVE_PIN, INPUT); 
  digitalWrite(MASTER_MOVE_PIN, HIGH);
  // PCintPort::attachInterrupt(MASTER_MOVE_PIN, &rising, RISING);
  enableInterrupt(MASTER_MOVE_PIN, calc_pwm_value1, CHANGE);

  pinMode(MASTER_SPIN_PIN, INPUT);
  digitalWrite(MASTER_SPIN_PIN, HIGH);
  // PCintPort::attachInterrupt(MASTER_SPIN_PIN, &rising, RISING);
  enableInterrupt(MASTER_SPIN_PIN, calc_pwm_value2, CHANGE);

  pinMode(SLAVE_MOVE_PIN, INPUT);
  digitalWrite(SLAVE_MOVE_PIN, HIGH);
  // PCintPort::attachInterrupt(SLAVE_MOVE_PIN, &rising, RISING);
  enableInterrupt(SLAVE_MOVE_PIN, calc_pwm_value3, CHANGE);

  pinMode(SLAVE_SPIN_PIN, INPUT);
  digitalWrite(SLAVE_SPIN_PIN, HIGH);
  // PCintPort::attachInterrupt(SLAVE_SPIN_PIN, &rising, RISING);
  enableInterrupt(SLAVE_SPIN_PIN, calc_pwm_value4, CHANGE);

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

  blockServo.write(min_servo_pulse);
  clampServo.write(3100 - min_servo_pulse);

  // initialize serial communication:
  Serial.begin(115200);

  // init master and slave module
  // triggerEvent(1, 1);
  // triggerEvent(2, 1);
}

void initMaster() {
  if (master_limit_triggered) {
    // wait for pulling back complete and set init status
    if (master_remain_steps <= 0) {
      master_limit_triggered = false;
      master_status = 1;
      master_tracing = 0;
      master_tracing_times = 0;
      master_module_position = 0;
    }
  } else {
    if (digitalRead(MASTER_LIMIT_PIN) == LOW) {
      // pull back
      master_limit_triggered = true;
      master_remain_steps = pullback_steps;
      master_last_direction = 1;
      digitalWrite(MASTER_DIR_PIN, HIGH);
    } else {
      master_last_direction = 0;
      master_remain_steps = minSteps;
      digitalWrite(MASTER_DIR_PIN, LOW);
    }
  }
}

void initSlave() {
  if (slave_limit_triggered) {
    // wait for pulling back complete and set init status
    if (slave_remain_steps <= 0) {
      slave_limit_triggered = false;
      slave_status = 1;
      slave_tracing = 0;
      slave_tracing_times = 0;
      slave_module_position = 0;
    }
  } else {
    if (digitalRead(SLAVE_LIMIT_PIN) == LOW) {
      // pull back
      slave_limit_triggered = true;
      slave_remain_steps = pullback_steps;
      slave_last_direction = 0;
      digitalWrite(SLAVE_DIR_PIN, LOW);
    } else {
      slave_last_direction = 1;
      slave_remain_steps = minSteps;
      digitalWrite(SLAVE_DIR_PIN, HIGH);
    }
  }  
}

void pullbackMaster() {
  if (master_remain_steps <= 0) {
    master_status = 0;
    master_limit_triggered = false;
  }
}

void pullbackSlave() {
  if (slave_remain_steps <= 0) {
    slave_status = 0;
    slave_limit_triggered = false;
  }
}

void trackMaster(int input) {
  // check limit and stop tracking
  bool forwardLimit = false;
  bool backwardLimit = false;
  if ((master_module_position < 0 ||  digitalRead(MASTER_LIMIT_PIN) == LOW) && !master_last_direction)  {
    backwardLimit = true;
    master_tracing = 0;
    master_tracing_times = 0;
    master_remain_steps = 0;
  } else if ((master_module_position > track_length  || digitalRead(MASTER_LIMIT_PIN) == LOW) && master_last_direction) {
    forwardLimit = true;
    master_remain_steps = 0;
  }

  //
  if (input < -156 && !forwardLimit && master_tracing) {
    // trace all along forward
    if (master_last_direction == 0) {
          master_last_direction = 1; 
          digitalWrite(MASTER_DIR_PIN, HIGH);     
    }
    if (master_remain_steps < 100) {
        master_remain_steps = 800;         
    }
  } else if (input > 150 && !backwardLimit && master_tracing){
    // tracing all along backward
    if (master_last_direction == 1) {
          master_last_direction = 0; 
          digitalWrite(MASTER_DIR_PIN, LOW);     
    }
    if (master_remain_steps < 100) {
        master_remain_steps = 800;        
    }
  } else if (input >= -150 && input <= 150) {
    if (!master_tracing) {
      master_tracing_times ++;      
      if (master_tracing_times >= tracing_ok_times) {
        master_tracing = 1;
        master_tracing_times = 0;
      }
    }
    // check 
    if (input < 0 - trackThreshold && !forwardLimit) {
      // track forward
      if (master_last_direction == 0) {
        master_last_direction = 1;
        digitalWrite(MASTER_DIR_PIN, HIGH);
      }
      master_remain_steps = minSteps;
    } else if (input > trackThreshold && !backwardLimit) {
      // track backward
      if (master_last_direction == 1) {
        master_last_direction = 0;
        digitalWrite(MASTER_DIR_PIN, LOW);
      }
      master_remain_steps = minSteps;        
    }
  }  
}

void trackSlave(int input) {
  // check limit and stop tracking
  bool forwardLimit = false;
  bool backwardLimit = false;
  if ((slave_module_position < 0 ||  digitalRead(SLAVE_LIMIT_PIN) == LOW) && slave_last_direction)  {
    backwardLimit = true;
    slave_tracing = 0;
    slave_tracing_times = 0;
    slave_remain_steps = 0;
  } else if ((slave_module_position > track_length  || digitalRead(SLAVE_LIMIT_PIN) == LOW) && !slave_last_direction) {
    forwardLimit = true;
    slave_remain_steps = 0;
  }

  //
  if (input < -150 && !backwardLimit && slave_tracing) {
    // trace all along backward
    if (slave_last_direction == 0) {
        slave_last_direction = 1; 
        digitalWrite(SLAVE_DIR_PIN, HIGH);     
    }
    if (slave_remain_steps < 100) {
        slave_remain_steps = 800;         
    }
  } else if (input > 150 && !forwardLimit && slave_tracing){
    // tracing all along forward
    if (slave_last_direction == 1) {
        slave_last_direction = 0; 
        digitalWrite(SLAVE_DIR_PIN, LOW);     
    }
    if (slave_remain_steps < 100) {
        slave_remain_steps = 800;        
    }
  } else if (input >= -150 && input <= 150) {
    if (!slave_tracing) {
      slave_tracing_times ++;      
      if (slave_tracing_times >= tracing_ok_times) {
        slave_tracing = 1;
        slave_tracing_times = 0;
      }
    }
    // check 
    if (input < 0 - trackThreshold && !backwardLimit) {
      // track backward
      if (slave_last_direction == 0) {
        slave_last_direction = 1;
        digitalWrite(SLAVE_DIR_PIN, HIGH);
      }
      slave_remain_steps = minSteps;
    } else if (input > trackThreshold && !forwardLimit) {
      // track forward
      if (slave_last_direction == 1) {
        slave_last_direction = 0;
        digitalWrite(SLAVE_DIR_PIN, LOW);
      }
      slave_remain_steps = minSteps;        
    }
  }     
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
    master_limit_triggered = digitalRead(MASTER_LIMIT_PIN) == LOW;
    if (master_limit_triggered) {
      // forward limit triggered, need to pull back
      master_status = 3;
      master_remain_steps = pullback_steps;      
      master_last_direction = 0;
      digitalWrite(MASTER_DIR_PIN, LOW);
    } else {
      master_status = 0;
    }
    // loose module
    triggerEvent(11, 0);
    triggerEvent(12, 0);
  } else if (type == 2) {
    // init slave tracker
    slave_limit_triggered = digitalRead(SLAVE_LIMIT_PIN) == LOW;
    if (slave_limit_triggered) {
      // forward limit triggered, need to pull back
      slave_status = 3;
      slave_remain_steps = pullback_steps;      
      slave_last_direction = 1;
      digitalWrite(SLAVE_DIR_PIN, HIGH);
    } else {
      slave_status = 0;
    }
  } else if (type == 11) {
    // trigger clamp
    clampServo.write(map(param, 0, 100, 3100 - min_servo_pulse, 3100 - max_servo_pulse));
  } else if (type == 12) {
    // triger block
    blockServo.write(map(param, 0, 100, min_servo_pulse, max_servo_pulse));  
  }
}

int test_master_position = 0;
float test_master_rotation = 0;
int test_slave_position = 0;
float test_slave_rotation = 0;

void loop(){
  // read sensors at a constant speed
  currTime = micros();
  
  // track visions
  if (master_status == 1) {
    // get vision position
    // master_move_value = map(master_move_pwm_value * 1.0 / 100.0, 1, 99, 0, 320) - 160;
    // master_spin_value = map(master_spin_pwm_value * 1.0 / 100.0, 1, 99, 0, 360);
    master_move_value = map(pwm_values[0] * 1.0 / 100.0, 1, 99, 0, 320) - 160;
    master_spin_value = map(pwm_values[1] * 1.0 / 100.0, 1, 99, 0, 360);
    // track master module
    trackMaster(master_move_value);    
  }
  if (slave_status == 1) {
    // get vision position
    // slave_move_value = map(slave_move_pwm_value * 1.0 / 100.0, 1, 99, 0, 320) - 160;
    // slave_spin_value = map(slave_spin_pwm_value * 1.0 / 100.0, 1, 99, 0, 360);
    slave_move_value = map(pwm_values[2] * 1.0 / 100.0, 1, 99, 0, 320) - 160;
    slave_spin_value = map(pwm_values[3] * 1.0 / 100.0, 1, 99, 0, 360) ;   
    // track slave module
    trackSlave(slave_move_value);    
  }

  // init modules
  if (currTime - prevInitTime > initInterval) {
    if (master_status == 0) {
      // init master module
      initMaster();
    } else if (master_status == 3) {
      pullbackMaster();
    }
    
    if (slave_status == 0) {
      initSlave();
    } else if (slave_status == 3) {
      pullbackSlave();
    }
    
    prevInitTime = currTime;

    /*
    Serial.print("master tracing: ");
    Serial.print(master_tracing);
    Serial.print(", slave tracing: ");
    Serial.print(slave_tracing);    
    Serial.print(", master move value: ");
    Serial.print(master_move_value);
    Serial.print(", master spin value: ");
    Serial.print(master_spin_value);    
    Serial.print(", slave move value: ");
    Serial.print(slave_move_value);
    Serial.print(", slave spin value: ");
    Serial.println(slave_spin_value);     */ 
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

     /*
    test_master_position += 5;
    if (test_master_position >= 7500) {
      test_master_position = 0;
    }

    test_master_rotation += 1;
    if (test_master_rotation > 360) {
      test_master_rotation = 0;
    }

    test_slave_position += 5;
    if (test_slave_position >= 7500) {
      test_slave_position = 0;
    }

    test_slave_rotation += 1;
    if (test_slave_rotation > 360) {
      test_slave_rotation = 0;
    }*/
    
    int master_position = 10 * (master_module_position - (master_move_value * 1.0 * master_vision_width / 320));
    // Serial.println(master_position);
    master_position = master_position >= 0 ? (int)master_position : 0;
    int slave_position = 10 * (slave_module_position - (slave_move_value * 1.0 * slave_vision_width / 320));
    // Serial.println(master_position);
    slave_position = slave_position >= 0 ? (int)slave_position : 0;    
    currSendBuffer[0] = '^';
    currSendBuffer[1] = lowByte(master_position);
    currSendBuffer[2] = highByte(master_position);
    currSendBuffer[3] = lowByte(master_spin_value);
    currSendBuffer[4] = highByte(master_spin_value);
    currSendBuffer[5] = lowByte(slave_position);
    currSendBuffer[6] = highByte(slave_position);
    currSendBuffer[7] = lowByte(slave_spin_value);
    currSendBuffer[8] = lowByte(slave_spin_value);
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





