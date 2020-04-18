#include <Wire.h>
#include <EEPROM.h>
#include <Adafruit_PWMServoDriver.h>

#define SERVOMIN 150 // this is the 'minimum' pulse length count (out of 4096) 
#define SERVOMAX 600 // this is the 'maximum' pulse length count (out of 4096)

#define FORCE_PIN1 4
#define FORCE_PIN2 5
#define FORCE_PIN3 6

#define CONTRAST_PIN A4
#define PRESSURE_PIN A5

#define SWITCH_PIN1 11
#define SWITCH_PIN2 12

#define HEARTBEAT_PIN 13

#define HALL_PIN1 A0
#define HALL_PIN2 A1
#define HALL_PIN3 A2

// constants
const int max_pressure = 1000;  // 1000Kpa = 1MPa

// eep rom structs total Bytes: 26
struct AdvConfig {
  byte min_hall_diam1;    //1
  int min_hall_value1;    //2
  byte max_hall_diam1;    //1
  int max_hall_value1;    //2
  byte min_hall_diam2;    //1
  int min_hall_value2;    //2
  byte max_hall_diam2;    //1
  int max_hall_value2;    //2
  byte min_hall_diam3;    //1
  int min_hall_value3;    //2
  byte max_hall_diam3;    //1
  int max_hall_value3;    //2
   
  byte min_servo_angle1;  //1
  byte max_servo_angle1;  //1
  byte min_servo_angle2;  //1
  byte max_servo_angle2;  //1
  byte min_servo_angle3;  //1
  byte max_servo_angle3;  //1
  
  int contrast_threshold; //2
};

union AdvConfigUnion {
   AdvConfig adv_confg;
   byte adv_config_bytes[26];
};

// device data
struct DeviceData {
  int hall_value1;        //2
  int hall_value2;        //2
  int hall_value3;        //2
  int pressure;           //2
  int contrast;           //2
  byte switch1;           //1
  byte switch2;           //1
  byte servo_angle1;      //1
  byte servo_angle2;      //1  
  byte servo_angle3;      //1
};

union DeviceDataUnion {
  DeviceData device_data;
  byte device_data_bytes[15];
};

// action data
struct ServoSetData {
  byte index;             //1
  byte angle;             //1
};

union ServoSetDataUnion {
  ServoSetData servo_set_data;
  byte servo_set_data_bytes[2];
};

AdvConfigUnion adv_config_union;
DeviceDataUnion device_data_union;
ServoSetDataUnion servo_set_data_union;

// variables
Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver(0x40);

unsigned long currTime = 0;
unsigned long readTime = 0;
unsigned long sendTime = 0;

const unsigned long readInterval = 30;
const unsigned long sendInterval = 30;

byte recvBuffer[6];
int recvBufferIndex = 0;
byte SendBuffer[15];
byte currSendBuffer[18];

// servo operation
// -----------------------------------------------------------
void Servo_180(int num, int degree) {
  long us = (degree * 1800 / 180 + 600);  // 0.6 ~ 2.4
  long pwmvalue = us * 4096 / 20000;      // 50hz: 20,000 us pwm.setPWM(num, 0, pwmvalue);
}

// -----------------------------------------------------------
void setup(){//将步进电机用到的IO管脚设置成输出

  pwm.begin(); 
  pwm.setPWMFreq(60);

  pinMode(PRESSURE_PIN, INPUT);
  pinMode(CONTRAST_PIN, INPUT);
  
  pinMode(SWITCH_PIN1, INPUT_PULLUP);
  pinMode(SWITCH_PIN2, INPUT_PULLUP);

  pinMode(HALL_PIN1, INPUT);
  pinMode(HALL_PIN2, INPUT);
  pinMode(HALL_PIN3, INPUT);
  
  pinMode(HEARTBEAT_PIN, OUTPUT);
  digitalWrite(HEARTBEAT_PIN, LOW);

  // load EEPROM data
  loadAdvConfig();

  // loose all servos according to adv config
  looseAllServos();
  
  // initialize serial communication:
  Serial.begin(115200);
}

// -----------------------------------------------------------
void loadAdvConfig() {
  EEPROM.get(0, adv_config_union);
}

// -----------------------------------------------------------
int readPressure() {
  int val = analogRead(PRESSURE_PIN);
  return abs(map(val, 0, 1023, 0, max_pressure));
}

// -----------------------------------------------------------
int readContrast() {
  return analogRead(CONTRAST_PIN);
}

// -----------------------------------------------------------
byte readSwitchStatus1() {
  return digitalRead(SWITCH_PIN1) == LOW;
}

// -----------------------------------------------------------
byte readSwitchStatus2(){
  return digitalRead(SWITCH_PIN2) == LOW;  
}

// -----------------------------------------------------------
int readHall1() {
  return analogRead(HALL_PIN1);
}

// -----------------------------------------------------------
int readHall2() {
  return analogRead(HALL_PIN2);
}

// -----------------------------------------------------------
int readHall3() {
  return analogRead(HALL_PIN3);
}

// -----------------------------------------------------------
void looseAllServos(){
  Servo_180(0, adv_config_union.adv_confg.min_servo_angle1);
  Servo_180(1, adv_config_union.adv_confg.min_servo_angle2);
  Servo_180(2, adv_config_union.adv_confg.min_servo_angle3); 
  delay(500);
}

// -----------------------------------------------------------
void loop(){
  // read sensors at a constant speed
  currTime = millis();
  
  if (currTime - prevReadTime > readInterval) {
    // read pressure();
    pressure = readPressure();
    // read contrast()
    contrast = readContrast();
    // read switch status
    switchStatus1 = readSwitchStatus1();    
    switchStatus2 = readSwitchStatus2();
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
    master_position = master_position >= 0 && master_tracing ? (int)master_position : 0;
    int slave_position = 10 * (slave_module_position + (slave_move_value * 1.0 * slave_vision_width / 320));
    // Serial.println(master_position);
    slave_position = slave_position >= 0 && slave_tracing ? (int)slave_position : 0;    
    currSendBuffer[0] = '^';
    if (heartbeat_status == 0) {
      currSendBuffer[1] = 0;
    } else {
      if (init_status == 0) {
        currSendBuffer[1] = 1;
      } else if (init_status == 1) {
        currSendBuffer[1] = 2;
      } else if (init_status == 2) {
        currSendBuffer[1] = 3;
      } else {
        currSendBuffer[1] = 255;
      }  
    }
    currSendBuffer[2] = lowByte(master_position);
    currSendBuffer[3] = highByte(master_position);
    currSendBuffer[4] = lowByte(master_spin_value);
    currSendBuffer[5] = highByte(master_spin_value);
    currSendBuffer[6] = lowByte(slave_position);
    currSendBuffer[7] = highByte(slave_position);
    currSendBuffer[8] = lowByte(slave_spin_value);
    currSendBuffer[9] = highByte(slave_spin_value);
    currSendBuffer[10] = lowByte(contrast);
    currSendBuffer[11] = highByte(contrast);
    currSendBuffer[12] = lowByte(pressure);
    currSendBuffer[13] = highByte(pressure);
    currSendBuffer[14] = switchStatus1;
    currSendBuffer[15] = switchStatus2;
    currSendBuffer[16] = '\n';

    if (memcmp(currSendBuffer, prevSendBuffer, 17) != 0) {
      Serial.write(currSendBuffer, 17);
      memcpy(prevSendBuffer, currSendBuffer, 17);
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

    // check heartbeat
    check_heartbeat();
    
    // update prev read time
    
    prevReadTime = currTime;    

  }
  
}
