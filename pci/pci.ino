#include <EnableInterrupt.h>
#include <ServoTimer2.h>  // the servo library

#define FORCE_PIN1 11
#define FORCE_PIN2 10
#define FORCE_PIN3 111


#define MASTER_STP_PIN       54
#define MASTER_DIR_PIN       55
#define MASTER_ENA_PIN       38

#define SLAVE_STP_PIN        60
#define SLAVE_DIR_PIN        61
#define SLAVE_ENA_PIN        56

#define CONTRAST_PIN A1
#define PRESSURE_PIN A2
#define SWITCH_PIN A3

// #define MASTER_SETTING_PIN A4 // set tracking color for master module
// #define SLAVE_SETTING_PIN A5 // set tracking color for slave module

#define MASTER_MOVE_PIN1 1 // get master move pwm pulse
#define MASTER_MOVE_PIN2 0 //
#define MASTER_SPIN_PIN 57 // get master spin pwm pulse
#define MASTER_SETTING_PIN1 58// 
#define MASTER_SETTING_PIN2 63// 

#define SLAVE_MOVE_PIN1 40 // get slave move pwm pulse
#define SLAVE_MOVE_PIN2 42
#define SLAVE_SPIN_PIN 65 // get slave spin pwm pulse
#define SLAVE_SETTING_PIN1 59 //
#define SLAVE_SETTING_PIN2 64 // 


#define MASTER_LIMIT_PIN 3 // master limit switch pin
#define SLAVE_LIMIT_PIN 2 // slave limit switch pin
#define FORCE_LIMIT_PIN2 14
#define FORCE_LIMIT_PIN3 15

#define PWM_PINS  6

#define PWM_CH1  0
#define PWM_CH2  1
#define PWM_CH3  2
#define PWM_CH4  3
#define PWM_CH4  4
#define PWM_CH4  5

volatile int prev_times[PWM_PINS];
volatile int pwm_values[PWM_PINS];

const int stepperSpeed = 400;
const int stepsPerRev = 1600;
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
const int trackThreshold = 100;

// servo constants
const int min_servo_pulse = 1120;
const int max_servo_pulse = 1750; 

volatile boolean master_toggle = 1;
volatile int master_speed_rate = 1;
volatile int master_speed_countdown = 0;

volatile boolean slave_toggle = 1;
volatile int slave_speed_rate = 1;
volatile int slave_speed_countdown = 0;

const int track_length = 750; 
const int pullback_steps = 800;

ServoTimer2 forceServo1;
ServoTimer2 forceServo2; 
ServoTimer2 forceServo3;

volatile int master_last_direction = 1;
volatile int slave_last_direction = 1;
int master_tracing = 0;
int master_tracing_times = 0;
int slave_tracing = 0;
int slave_tracing_times = 0;
const int tracing_ok_times = 100;

int master_seeking = 0;
int master_seeking_times = 0;
int slave_seeking = 0;
int slave_seeking_times = 0;
const int seeking_prep_times = 20;

int currTime = 0;
int prevReadTime = 0;
int prevInitTime = 0;
int readInterval = 30000;
int initInterval = 3000;
int pressure = 0;
int contrast = 0;
byte switchStatus = 0;
byte readBuffer[6];
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
  if (master_remain_steps > 0) {
    if (master_speed_rate > 1) {
      if (master_speed_countdown > 0) {
        master_speed_countdown --;        
        return;
      } else if (master_speed_countdown == 0) {
        master_speed_countdown = master_speed_rate;
      }
    }
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

ISR(TIMER3_COMPA_vect){
  if (slave_remain_steps > 0) {
    if (slave_speed_rate > 1) {
      if (slave_speed_countdown > 0) {
        slave_speed_countdown --;        
        return;
      } else if (slave_speed_countdown == 0) {
        slave_speed_countdown = slave_speed_rate;
      }
    }    
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
}

void calc_pwm(int channel, int pwm_pin) {
  if (digitalRead(pwm_pin) == HIGH) {
    prev_times[channel] = micros();
  } else {
    pwm_values[channel] = micros() - prev_times[channel] + 40;
  }
}

void calc_pwm_value1() { calc_pwm(PWM_CH1, MASTER_MOVE_PIN1); }
void calc_pwm_value2() { calc_pwm(PWM_CH2, MASTER_SPIN_PIN); }
void calc_pwm_value3() { calc_pwm(PWM_CH3, SLAVE_MOVE_PIN1); }
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
  pinMode(FORCE_LIMIT_PIN2, INPUT_PULLUP);
  pinMode(FORCE_LIMIT_PIN3, INPUT_PULLUP);
  
  pinMode(MASTER_DIR_PIN, OUTPUT);
  digitalWrite(MASTER_DIR_PIN, HIGH);
  pinMode(MASTER_STP_PIN, OUTPUT);
  pinMode(MASTER_ENA_PIN, OUTPUT);
  digitalWrite(MASTER_ENA_PIN, LOW);

  pinMode(SLAVE_DIR_PIN, OUTPUT);
  digitalWrite(MASTER_DIR_PIN, HIGH);
  pinMode(SLAVE_STP_PIN, OUTPUT);
  pinMode(SLAVE_ENA_PIN, OUTPUT);
  digitalWrite(SLAVE_ENA_PIN, LOW);  

  pinMode(FORCE_PIN1, OUTPUT);
  pinMode(FORCE_PIN2, OUTPUT);
  pinMode(FORCE_PIN3, OUTPUT);

  pinMode(MASTER_SETTING_PIN1, OUTPUT);  
  pinMode(MASTER_SETTING_PIN2, OUTPUT);
  pinMode(SLAVE_SETTING_PIN1, OUTPUT);
  pinMode(SLAVE_SETTING_PIN2, OUTPUT);

  pinMode(MASTER_MOVE_PIN1, INPUT); 
  digitalWrite(MASTER_MOVE_PIN1, HIGH);
  // PCintPort::attachInterrupt(MASTER_MOVE_PIN1, &rising, RISING);
  enableInterrupt(MASTER_MOVE_PIN1, calc_pwm_value1, CHANGE);

  pinMode(MASTER_SPIN_PIN, INPUT);
  digitalWrite(MASTER_SPIN_PIN, HIGH);
  // PCintPort::attachInterrupt(MASTER_SPIN_PIN, &rising, RISING);
  enableInterrupt(MASTER_SPIN_PIN, calc_pwm_value2, CHANGE);

  pinMode(SLAVE_MOVE_PIN1, INPUT);
  digitalWrite(SLAVE_MOVE_PIN1, HIGH);
  // PCintPort::attachInterrupt(SLAVE_MOVE_PIN1, &rising, RISING);
  enableInterrupt(SLAVE_MOVE_PIN1, calc_pwm_value3, CHANGE);

  pinMode(SLAVE_SPIN_PIN, INPUT);
  digitalWrite(SLAVE_SPIN_PIN, HIGH);
  // PCintPort::attachInterrupt(SLAVE_SPIN_PIN, &rising, RISING);
  enableInterrupt(SLAVE_SPIN_PIN, calc_pwm_value4, CHANGE);

  cli();//stop interrupts

  // timer 1
  TCCR1A = 0;// set entire TCCR1A register to 0
  TCCR1B = 0;// same for TCCR1B
  TCNT1  = 0;//initialize counter value to 0
  // set compare match register for 1hz increments
  OCR1A = 16000000.0f / 10000;
  // turn on CTC mode
  TCCR1B |= (1 << WGM12);
  // Set CS12 and CS10 bits for 1024 prescaler
  TCCR1B |= (1 << CS10); // no prescaler
  // enable timer compare interrupt
  TIMSK1 |= (1 << OCIE1A);

  // timer 3 
  TCCR3A = 0;// set entire TCCR1A register to 0
  TCCR3B = 0;// same for TCCR1B
  TCNT3  = 0;//initialize counter value to 0
  // set compare match register for 1hz increments
  OCR3A = 16000000.0f / 10000;
  // turn on CTC mode
  TCCR3B |= (1 << WGM12);
  // Set CS12 and CS10 bits for 1024 prescaler
  TCCR3B |= (1 << CS30); // no prescaler
  // enable timer compare interrupt
  TIMSK3 |= (1 << OCIE3A);
  
  sei();//allow interrupts

  forceServo1.attach(FORCE_PIN1);
  forceServo2.attach(FORCE_PIN2);
  forceServo3.attach(FORCE_PIN3);

  forceServo1.write(min_servo_pulse);
  forceServo2.write(min_servo_pulse);
  forceServo3.write(min_servo_pulse);

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
      // reset master speed rate
      master_speed_rate = 1;
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
      // reset slave speed rate
      slave_speed_rate = 1;      
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
    master_seeking = 0;
    master_tracing_times = 0;
    master_remain_steps = 0;
  } else if ((master_module_position > track_length  || digitalRead(MASTER_LIMIT_PIN) == LOW) && master_last_direction) {
    forwardLimit = true;
    master_seeking = 0;
    master_remain_steps = 0;
  }

  //
  if (input < -156 && !forwardLimit && master_tracing) {
    if (!master_seeking) {
      master_seeking_times ++;
      if (master_seeking_times >= seeking_prep_times) {
        master_seeking = 1;
        master_seeking_times = 0;
      } 
      return;
    }
    // trace all along forward
    if (master_last_direction == 0) {
          master_last_direction = 1; 
          digitalWrite(MASTER_DIR_PIN, HIGH);     
    }
    if (master_remain_steps < 100) {
        master_remain_steps = 800;         
    }
  } else if (input > 150 && !backwardLimit && master_tracing){
    if (!master_seeking) {
      master_seeking_times ++;
      if (master_seeking_times >= seeking_prep_times) {
        master_seeking = 1;
        master_seeking_times = 0;
      } 
      return;
    }
    // tracing all along backward
    if (master_last_direction == 1) {
          master_last_direction = 0; 
          digitalWrite(MASTER_DIR_PIN, LOW);     
    }
    if (master_remain_steps < 100) {
        master_remain_steps = 800;        
    }
  } else if (input >= -150 && input <= 150) {
    master_seeking = 0;
    master_seeking_times = 0;
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
    slave_seeking = 0;
    slave_tracing = 0;
    slave_tracing_times = 0;
    slave_remain_steps = 0;
  } else if ((slave_module_position > track_length  || digitalRead(SLAVE_LIMIT_PIN) == LOW) && !slave_last_direction) {
    forwardLimit = true;
    slave_seeking = 0;
    slave_remain_steps = 0;
  }

  //
  if (input < -150 && !backwardLimit && slave_tracing) {
    if (!slave_seeking) {
      slave_seeking_times ++;
      if (slave_seeking_times >= seeking_prep_times) {
        slave_seeking = 1;
        slave_seeking_times = 0;
      } 
      return;
    }

    // trace all along backward
    if (slave_last_direction == 0) {
        slave_last_direction = 1; 
        digitalWrite(SLAVE_DIR_PIN, HIGH);     
    }
    if (slave_remain_steps < 100) {
        slave_remain_steps = 800;         
    }
  } else if (input > 150 && !forwardLimit && slave_tracing){
    if (!slave_seeking) {
      slave_seeking_times ++;
      if (slave_seeking_times >= seeking_prep_times) {
        slave_seeking = 1;
        slave_seeking_times = 0;
      } 
      return;
    }
    // tracing all along forward
    if (slave_last_direction == 1) {
        slave_last_direction = 0; 
        digitalWrite(SLAVE_DIR_PIN, LOW);     
    }
    if (slave_remain_steps < 100) {
        slave_remain_steps = 800;        
    }
  } else if (input >= -150 && input <= 150) {
    slave_seeking = 0;
    slave_seeking_times = 0;
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

void triggerEvent(char type, char param1, int param2) {
  if (type == 1) {
    // loose force servos
    triggerEvent(2, 1, 0);
    triggerEvent(2, 2, 0);
    if (param1 == 0) {
      // init all
      triggerEvent(1, 1, 0);
      triggerEvent(1, 2, 0);
      triggerEvent(1, 3, 0);
    } else if (param1 == 1) {
      // slow down speed
      master_speed_rate = 3;
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
    } else if (param1 == 2) {
      // slow down speed
      slave_speed_rate = 3;
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
    } else if (param1 == 3) {
      // init forces
      
    }
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
        triggerEvent(readBuffer[1], readBuffer[2], readBuffer[3] + (readBuffer[4] << 8));       
      }
      if (readBufferIndex < 5) {
        readBuffer[readBufferIndex] = ch;
      }
      readBufferIndex ++;
    }
    
    // update prev read time
    
    prevReadTime = currTime;    

  }
  
}





