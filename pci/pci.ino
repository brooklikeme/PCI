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

#define FORCE_STP_PIN2       46
#define FORCE_DIR_PIN2       48
#define FORCE_ENA_PIN2       62

#define FORCE_STP_PIN3       36
#define FORCE_DIR_PIN3       34
#define FORCE_ENA_PIN3       30

#define CONTRAST_PIN A11
#define PRESSURE_PIN A12
#define SWITCH_PIN 16

// #define MASTER_SETTING_PIN A4 // set tracking color for master module
// #define SLAVE_SETTING_PIN A5 // set tracking color for slave module

#define MASTER_MOVE_PIN1 1 // get master move pwm pulse
#define MASTER_SPIN_PIN1 0 // get master spin pwm pulse
#define MASTER_SETTING_PIN1 57// 

#define MASTER_MOVE_PIN2 63 //
#define MASTER_SETTING_PIN2 59// 

#define SLAVE_MOVE_PIN1 40 // get slave move pwm pulse
#define SLAVE_SPIN_PIN1 42 // get slave spin pwm pulse
#define SLAVE_SETTING_PIN1 64 //

#define SLAVE_MOVE_PIN2 52
#define SLAVE_SETTING_PIN2 53 // 


#define MASTER_LIMIT_PIN 3 // master limit switch pin
#define SLAVE_LIMIT_PIN 2 // slave limit switch pin
#define FORCE_LIMIT_PIN2 14
#define FORCE_LIMIT_PIN3 19

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
const int distancePerRev = 60;
const int distancePerForceRev = 40;
const float distancePerStep = float(distancePerRev) / float(stepsPerRev);
const float distancePerForceStep = float(distancePerForceRev) / float(stepsPerRev);
const int master_vision_width = 60;
const int slave_vision_width = 60;
const int track_offset = 30;
const int force2_offset = 126;
const int force_distance = 18;
const int force3_offset = force2_offset + force_distance;
int minSteps = 80;// stepsPerRev / (distancePerRev * 2);
volatile int master_remain_steps = 0;
volatile int slave_remain_steps = 0;
volatile int force2_remain_steps = 0;
volatile int force3_remain_steps = 0;
volatile int master_move_pwm_value = 0;
volatile int master_spin_pwm_value = 0;
volatile int prev_time = 0;
volatile int slave_move_pwm_value = 0;
volatile int slave_spin_pwm_value = 0;
volatile int master_status = 1; // 0: initializing, 1: tracking
volatile int slave_status = 1;  // 0: initializing, 1: tracking
volatile int force2_status = 1;
volatile int force3_status = 1;
volatile float master_module_position = 0;
volatile float slave_module_position = 0;
volatile float force2_module_position = 0;
volatile float force3_module_position = 0;

float force2_setting_position = 0;
float force3_setting_position = 0;

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

volatile boolean force2_toggle = 1;
volatile boolean force3_toggle = 1;

const int track_length = 900; 
const int pullback_steps = 800;
const int force_pullback_steps = 80;

ServoTimer2 forceServo1;
ServoTimer2 forceServo2; 
ServoTimer2 forceServo3;

volatile int master_last_direction = 1;
volatile int slave_last_direction = 1;
volatile int force2_last_direction = 1;
volatile int force3_last_direction = 1;
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
bool force2_limit_triggered = false;
bool force3_limit_triggered = false;
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

ISR(TIMER4_COMPA_vect){

  if (force3_remain_steps > 0) {
    if (force3_toggle){
      digitalWrite(FORCE_STP_PIN3, HIGH);
    }
    else{
      digitalWrite(FORCE_STP_PIN3, LOW);
      force3_remain_steps --;       
      // update force3 module position
      force3_module_position += force3_last_direction ? distancePerForceStep : 0 - distancePerForceStep;
    }
    force3_toggle = !force3_toggle;   
  } 
    
  if (force2_remain_steps > 0) {
    if (force2_toggle){
      digitalWrite(FORCE_STP_PIN2, HIGH);
    }
    else{
      digitalWrite(FORCE_STP_PIN2, LOW);
      force2_remain_steps --;       
      // update force2 module position
      force2_module_position += force2_last_direction ? distancePerForceStep : 0 - distancePerForceStep;
    }
    force2_toggle = !force2_toggle;   
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
void calc_pwm_value2() { calc_pwm(PWM_CH2, MASTER_SPIN_PIN1); }
void calc_pwm_value3() { calc_pwm(PWM_CH3, MASTER_MOVE_PIN2); }
void calc_pwm_value4() { calc_pwm(PWM_CH4, SLAVE_MOVE_PIN1); }
void calc_pwm_value5() { calc_pwm(PWM_CH3, SLAVE_SPIN_PIN1); }
void calc_pwm_value6() { calc_pwm(PWM_CH4, SLAVE_MOVE_PIN2); }

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

  pinMode(FORCE_DIR_PIN2, OUTPUT);
  digitalWrite(FORCE_DIR_PIN2, HIGH);
  pinMode(FORCE_STP_PIN2, OUTPUT);
  pinMode(FORCE_ENA_PIN2, OUTPUT);
  digitalWrite(FORCE_ENA_PIN2, LOW);

  pinMode(FORCE_DIR_PIN3, OUTPUT);
  digitalWrite(FORCE_DIR_PIN3, HIGH);
  pinMode(FORCE_STP_PIN3, OUTPUT);
  pinMode(FORCE_ENA_PIN3, OUTPUT);
  digitalWrite(FORCE_ENA_PIN3, LOW);

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

  pinMode(MASTER_SPIN_PIN1, INPUT);
  digitalWrite(MASTER_SPIN_PIN1, HIGH);
  // PCintPort::attachInterrupt(MASTER_SPIN_PIN, &rising, RISING);
  enableInterrupt(MASTER_SPIN_PIN1, calc_pwm_value2, CHANGE);

  pinMode(MASTER_MOVE_PIN2, INPUT);
  digitalWrite(MASTER_MOVE_PIN2, HIGH);
  // PCintPort::attachInterrupt(MASTER_SPIN_PIN, &rising, RISING);
  enableInterrupt(MASTER_MOVE_PIN2, calc_pwm_value3, CHANGE);

  pinMode(SLAVE_MOVE_PIN1, INPUT);
  digitalWrite(SLAVE_MOVE_PIN1, HIGH);
  // PCintPort::attachInterrupt(SLAVE_MOVE_PIN1, &rising, RISING);
  enableInterrupt(SLAVE_MOVE_PIN1, calc_pwm_value4, CHANGE);

  pinMode(SLAVE_SPIN_PIN1, INPUT);
  digitalWrite(SLAVE_SPIN_PIN1, HIGH);
  // PCintPort::attachInterrupt(SLAVE_SPIN_PIN, &rising, RISING);
  enableInterrupt(SLAVE_SPIN_PIN1, calc_pwm_value5, CHANGE);

  pinMode(SLAVE_MOVE_PIN2, INPUT);
  digitalWrite(SLAVE_MOVE_PIN2, HIGH);
  // PCintPort::attachInterrupt(SLAVE_SPIN_PIN, &rising, RISING);
  enableInterrupt(SLAVE_MOVE_PIN2, calc_pwm_value6, CHANGE);

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

    // timer 4 
  TCCR4A = 0;// set entire TCCR1A register to 0
  TCCR4B = 0;// same for TCCR1B
  TCNT4  = 0;//initialize counter value to 0
  // set compare match register for 1hz increments
  OCR4A = 16000000.0f / 3000;
  // turn on CTC mode
  TCCR4B |= (1 << WGM12);
  // Set CS12 and CS10 bits for 1024 prescaler
  TCCR4B |= (1 << CS40); // no prescaler
  // enable timer compare interrupt
  TIMSK4 |= (1 << OCIE4A);
  
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
      master_module_position = track_offset;
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
      slave_module_position = track_offset;
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

void initForce2() {
  if (force2_limit_triggered) {
    // wait for pulling back complete and set init status
    if (force2_remain_steps <= 0) {
      force2_limit_triggered = false;
      force2_status = 1;
      force2_module_position = force2_offset;
      force3_status = 0;
    }
  } else {
    if (digitalRead(FORCE_LIMIT_PIN2) == LOW) {
      // pull back
      force2_limit_triggered = true;
      force2_remain_steps = force_pullback_steps;
      force2_last_direction = 1;
      digitalWrite(FORCE_DIR_PIN2, HIGH);
    } else {
      force2_last_direction = 0;
      force2_remain_steps = minSteps;
      digitalWrite(FORCE_DIR_PIN2, LOW);
    }
  }
}

void initForce3() {
  if (force3_limit_triggered) {
    // wait for pulling back complete and set init status
    if (force3_remain_steps <= 0) {
      force3_limit_triggered = false;
      force3_status = 1;
      force3_module_position = force3_offset;      
    }
  } else {
    if (digitalRead(FORCE_LIMIT_PIN3) == LOW) {
      // pull back
      force3_limit_triggered = true;
      force3_remain_steps = force_pullback_steps;
      force3_last_direction = 1;
      digitalWrite(FORCE_DIR_PIN3, HIGH);
    } else {
      force3_last_direction = 0;
      force3_remain_steps = minSteps;
      digitalWrite(FORCE_DIR_PIN3, LOW);
    }
  }
}

void moveForce2() {
  if (digitalRead(FORCE_LIMIT_PIN2) == LOW || force2_module_position >= force2_setting_position) {
    force2_remain_steps = 0;
    force2_status = 1;
  } else {
    force2_last_direction = 1;
    force2_remain_steps = minSteps;
    digitalWrite(FORCE_DIR_PIN2, HIGH);
  }  
}

void moveForce3() {
  if (digitalRead(FORCE_LIMIT_PIN3) == LOW || force3_module_position >= force3_setting_position) {
    force3_remain_steps = 0;
    force3_status = 1;
  } else {
    force3_last_direction = 1;
    force3_remain_steps = minSteps;
    digitalWrite(FORCE_DIR_PIN3, HIGH);
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

void pullbackForce2() {
  if (force2_remain_steps <= 0) {
    force2_status = 0;
    force2_limit_triggered = false;
  }
}

void pullbackForce3() {
  if (force3_remain_steps <= 0) {
    force3_status = 0;
    force3_limit_triggered = false;
  }
}

void trackMaster(int input) {
  // check limit and stop tracking
  bool forwardLimit = false;
  bool backwardLimit = false;
  if ((master_module_position < track_offset ||  digitalRead(MASTER_LIMIT_PIN) == LOW) && !master_last_direction)  {
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
  if ((slave_module_position < track_offset ||  digitalRead(SLAVE_LIMIT_PIN) == LOW) && slave_last_direction)  {
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
      // loose force servos
      triggerEvent(3, 1, 0);
      triggerEvent(3, 2, 0);
      triggerEvent(3, 3, 0);
      //wait for servo action
      delay(300);
      // init forces
      force2_limit_triggered = digitalRead(FORCE_LIMIT_PIN2) == LOW;
      if (force2_limit_triggered) {
        // forward limit triggered, need to pull back
        force2_status = 3;
        force2_remain_steps = force_pullback_steps;      
        force2_last_direction = 1;
        digitalWrite(FORCE_DIR_PIN2, HIGH);
      } else {
        force2_status = 0;
      }      
    }
  }
  else if (type == 2) {
    // set force positions
    if (param1 == 1) {
      // pre set force2 position
      force2_setting_position = param2 * 1.0 / 10;
    } else if (param1 == 2) {
      // pre set force3 position
      force3_setting_position = param2 * 1.0 / 10;
    } else if (param1 == 3) {
      // execute position setting
      // check if force2 and force3 positions are ok   
      if (force2_setting_position >= force2_offset 
        && force3_setting_position <= track_length 
        && force3_setting_position - force2_setting_position >= force_distance) {          
        // loose force servos
        triggerEvent(3, 1, 0);
        triggerEvent(3, 2, 0);
        triggerEvent(3, 3, 0);
        //wait for servo action
        delay(300);
        //
        force2_status = 2;
        force3_status = 2;        
      }
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
    slave_move_value = map(pwm_values[3] * 1.0 / 100.0, 1, 99, 0, 320) - 160;
    slave_spin_value = map(pwm_values[4] * 1.0 / 100.0, 1, 99, 0, 360) ;   
    // track slave module
    trackSlave(slave_move_value);    
  }

  // init modules
  if (currTime - prevInitTime > initInterval) {
    // init master module
    if (master_status == 0) {
      // init master module
      initMaster();
    } else if (master_status == 3) {
      pullbackMaster();
    }
    // init slave module
    if (slave_status == 0) {
      initSlave();
    } else if (slave_status == 3) {
      pullbackSlave();
    }
    // init force2 module
    if (force2_status == 0) {
      initForce2();
    } else if (force2_status == 3) {
      pullbackForce2();
    }

    // init force 3 module
    if (force3_status == 0) {
      initForce3();
    } else if (force3_status == 3) {
      pullbackForce3();
    }    

    // move force 2 module
    if (force2_status == 2) {
      moveForce2();
    }

    // move force3 module
    if (force3_status == 2) {
      moveForce3();
    }
    
    prevInitTime = currTime;
        
    
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
    Serial.println(slave_spin_value);    
  }
  
  if (currTime - prevReadTime > readInterval) {
    // read pressure();
    pressure = force3_module_position * 10; //readPressure();
    // read contrast()
    contrast = force2_module_position * 10; // readContrast();
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





