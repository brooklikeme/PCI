#include <Wire.h>
#include <EEPROM.h>
#include <Servo.h>

#define SERVO_PIN1 4
#define SERVO_PIN2 5
#define SERVO_PIN3 6

#define CONTRAST_PIN A4
#define PRESSURE_PIN A5

#define SWITCH_PIN1 11
#define SWITCH_PIN2 12

#define HEARTBEAT_PIN 13

#define HALL_PIN1 A0
#define HALL_PIN2 A1
#define HALL_PIN3 A2

Servo servo1; 
Servo servo2; 
Servo servo3; 

// constants
const int max_pressure = 1000;  // 1000Kpa = 1MPa

enum SERIAL_TYPE {
  DEVICE_DATA       = 0,
  ADV_CONFIG_DATA   = 1,
  SERVO_SET_DATA    = 2
};

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


unsigned long currTime = 0;
unsigned long sendTime = 0;

const unsigned long sendInterval = 1000;

AdvConfigUnion adv_config_union;
ServoSetDataUnion servo_set_data_union;

DeviceDataUnion device_data_union;
DeviceDataUnion device_data_union_prev;
byte recvBuffer[30];
int recvBufferIndex = 0;
SERIAL_TYPE serialType;

// servo operation
// -----------------------------------------------------------
void Servo_180(int num, int degree) {
  if (num == 0) {
    device_data_union.device_data.servo_angle1 = degree;
    servo1.write(device_data_union.device_data.servo_angle1);
  } else if (num == 1) {
    device_data_union.device_data.servo_angle2 = degree;
    servo2.write(device_data_union.device_data.servo_angle2);
  } else if (num == 2) {
    device_data_union.device_data.servo_angle3 = degree;
    servo3.write(device_data_union.device_data.servo_angle3);
  }
  delay(15);
}

// -----------------------------------------------------------
void setup(){//将步进电机用到的IO管脚设置成输出

  pinMode(PRESSURE_PIN, INPUT);
  pinMode(CONTRAST_PIN, INPUT);
  
  pinMode(SWITCH_PIN1, INPUT_PULLUP);
  pinMode(SWITCH_PIN2, INPUT_PULLUP);

  pinMode(HALL_PIN1, INPUT);
  pinMode(HALL_PIN2, INPUT);
  pinMode(HALL_PIN3, INPUT);
  
  pinMode(HEARTBEAT_PIN, OUTPUT);
  digitalWrite(HEARTBEAT_PIN, LOW);

  servo1.attach(SERVO_PIN1); 
  servo2.attach(SERVO_PIN2); 
  servo3.attach(SERVO_PIN3); 

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
void saveAdvConfig() {
  EEPROM.put(0, adv_config_union);
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
}

// -----------------------------------------------------------
void loop(){
  // read sensors at a constant speed
  currTime = millis();
  if (currTime - sendTime > sendInterval) {
    // read pressure();
    device_data_union.device_data.pressure = readPressure();
    // read contrast()
    device_data_union.device_data.contrast = readContrast();
    // read switch status
    device_data_union.device_data.switch1 = readSwitchStatus1(); 
    device_data_union.device_data.switch2 = readSwitchStatus2();
    // read hall data       
    device_data_union.device_data.hall_value1 = readHall1();
    device_data_union.device_data.hall_value2 = readHall2();
    device_data_union.device_data.hall_value3 = readHall3();

    Serial.print("hall1: ");
    Serial.print(device_data_union.device_data.hall_value1);
    Serial.print(", hall2: ");
    Serial.print(device_data_union.device_data.hall_value2);    
    Serial.print(", hall3: ");
    Serial.print(device_data_union.device_data.hall_value3);
    Serial.print(", pressure: ");
    Serial.print(device_data_union.device_data.pressure);
    Serial.print(", contrast: ");
    Serial.print(device_data_union.device_data.contrast);
    Serial.print(", switch1: ");
    Serial.print(device_data_union.device_data.switch1);
    Serial.print(", switch2: ");
    Serial.print(device_data_union.device_data.switch2);
    Serial.print(", servo_angle1: ");
    Serial.print(device_data_union.device_data.servo_angle1);            
    Serial.print(", servo_angle2: ");
    Serial.print(device_data_union.device_data.servo_angle2);  
    Serial.print(", servo_angle3: ");
    Serial.println(device_data_union.device_data.servo_angle3);          

    if (memcmp(device_data_union.device_data_bytes, device_data_union_prev.device_data_bytes, sizeof(device_data_union.device_data_bytes)) != 0) {
      Serial.write('^');  // start sysbol
      Serial.write(DEVICE_DATA);
      Serial.write(device_data_union.device_data_bytes, sizeof(device_data_union.device_data_bytes));
      Serial.write('\n');
      memcpy(device_data_union_prev.device_data_bytes, device_data_union.device_data_bytes, sizeof(device_data_union.device_data_bytes));
    }
   
    // read serial
    while (Serial.available() > 0) {
      // read the incoming byte:
      char ch = Serial.read();
      if (ch == '^') {
        recvBufferIndex = 0; 
      } else if (ch == '\n') {
        // get serial data according to serial type
        if (serialType == DEVICE_DATA) {
          // should not get device data, ignore
        } else if (serialType == SERVO_SET_DATA) {
          // get servo angle
          for (byte n = 0; n < sizeof(servo_set_data_union.servo_set_data_bytes); n++) {
            servo_set_data_union.servo_set_data_bytes[n] = recvBuffer[n];
          }
          // set servo angle
          Servo_180(servo_set_data_union.servo_set_data.index, servo_set_data_union.servo_set_data.angle);
        } else if (serialType == ADV_CONFIG_DATA) {
          if (recvBufferIndex > 10) {
            // update and save adv config data
            for (byte n = 0; n < sizeof(adv_config_union.adv_config_bytes); n++) {
              adv_config_union.adv_config_bytes[n] = recvBuffer[n];
            }
            saveAdvConfig();   
          } else {
            // send adv config data
            Serial.write('^');  // start sysbol
            Serial.write(ADV_CONFIG_DATA);
            Serial.write(adv_config_union.adv_config_bytes, sizeof(adv_config_union.adv_config_bytes));
            Serial.write('\n');
          }
        }
      }
      if (recvBufferIndex == 1) {
        serialType = ch;
      } else if (recvBufferIndex > 1 && recvBufferIndex < sizeof(recvBuffer)) {
        recvBuffer[recvBufferIndex - 1] = ch;
      }
      recvBufferIndex ++;
    }
    
    // update prev read time
    sendTime = currTime;    
  }
}
