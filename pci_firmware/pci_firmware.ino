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

//#define DEBUG

Servo servo1; 
Servo servo2; 
Servo servo3; 

// constants
const int max_pressure = 1000;  // 1000Kpa = 1MPa

enum SERIAL_TYPE {
  DEVICE_DATA       = 0,
  ADV_CONFIG_DATA   = 1,
  SERVO_SET_DATA    = 2,
  HEARTBEAT_DATA    = 3,
  SERIAL_NONE       = 100
};

// eep rom structs total Bytes: 26
struct AdvConfig {
  byte min_hall_diam1;     //1
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
   AdvConfig adv_config;
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

const unsigned long sendInterval = 200;

AdvConfigUnion adv_config_union;
ServoSetDataUnion servo_set_data_union;

DeviceDataUnion device_data_union;
DeviceDataUnion device_data_union_prev;
byte recvBuffer[30];
int recvBufferIndex = 0;
SERIAL_TYPE serialType;

unsigned long last_heartbeat_time = 0;
unsigned long heartbeat_interval = 10000;

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

  // adv_config_union.adv_config.min_hall_diam1 = 100;
  // saveAdvConfig();
  // delay(100);
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
#ifdef DEBUG
  return 500;
#else
  return analogRead(HALL_PIN1);
#endif
}

// -----------------------------------------------------------
int readHall2() {
#ifdef DEBUG
  return 500;
#else  
  return analogRead(HALL_PIN2);
#endif
}

// -----------------------------------------------------------
int readHall3() {
#ifdef DEBUG
  return 500;
#else  
  return analogRead(HALL_PIN3);
#endif
}

void check_heartbeat()
{
  if (last_heartbeat_time == 0 || (millis() - last_heartbeat_time > heartbeat_interval)) {
    looseAllServos();
    digitalWrite(HEARTBEAT_PIN, LOW);
  } else {
    digitalWrite(HEARTBEAT_PIN, HIGH);
  }
}

// -----------------------------------------------------------
void looseAllServos(){
  Servo_180(0, adv_config_union.adv_config.min_servo_angle1);
  Servo_180(1, adv_config_union.adv_config.min_servo_angle2);
  Servo_180(2, adv_config_union.adv_config.min_servo_angle3); 
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

/*
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

    Serial.print("min_hall_diam1: ");
    Serial.print(adv_config_union.adv_config.min_hall_diam1);
    Serial.print(", min_hall_value1: ");
    Serial.print(adv_config_union.adv_config.min_hall_value1);    
    Serial.print(", max_hall_diam1: ");
    Serial.print(adv_config_union.adv_config.max_hall_diam1);
    Serial.print(", max_hall_value1: ");
    Serial.print(adv_config_union.adv_config.max_hall_value1);
    
    Serial.print("min_hall_diam2: ");
    Serial.print(adv_config_union.adv_config.min_hall_diam2);
    Serial.print(", min_hall_value2: ");
    Serial.print(adv_config_union.adv_config.min_hall_value2);    
    Serial.print(", max_hall_diam2: ");
    Serial.print(adv_config_union.adv_config.max_hall_diam2);
    Serial.print(", max_hall_value2: ");
    Serial.print(adv_config_union.adv_config.max_hall_value2);

    Serial.print("min_hall_diam3: ");
    Serial.print(adv_config_union.adv_config.min_hall_diam3);
    Serial.print(", min_hall_value3: ");
    Serial.print(adv_config_union.adv_config.min_hall_value3);    
    Serial.print(", max_hall_diam3: ");
    Serial.print(adv_config_union.adv_config.max_hall_diam3);
    Serial.print(", max_hall_value3: ");
    Serial.print(adv_config_union.adv_config.max_hall_value3);

    Serial.print(", min_servo_angle1: ");
    Serial.print(adv_config_union.adv_config.min_servo_angle1);
    Serial.print(", max_servo_angle1: ");
    Serial.print(adv_config_union.adv_config.max_servo_angle1);
    Serial.print(", min_servo_angle2: ");
    Serial.print(adv_config_union.adv_config.min_servo_angle2);
    Serial.print(", max_servo_angle2: ");
    Serial.print(adv_config_union.adv_config.max_servo_angle2);
    Serial.print(", min_servo_angle3: ");
    Serial.print(adv_config_union.adv_config.min_servo_angle3);
    Serial.print(", max_servo_angle3: ");
    Serial.print(adv_config_union.adv_config.max_servo_angle3);        

    Serial.print(", contrast_threshold: ");
    Serial.println(adv_config_union.adv_config.contrast_threshold);   

    */

    if (memcmp(device_data_union.device_data_bytes, device_data_union_prev.device_data_bytes, sizeof(device_data_union.device_data_bytes)) != 0) {
      Serial.write(DEVICE_DATA);
      Serial.write(device_data_union.device_data_bytes, sizeof(device_data_union.device_data_bytes));
      Serial.write('+');  // end sysbol
      Serial.write('+');  // end sysbol
      Serial.write('+');  // end sysbol      
      memcpy(device_data_union_prev.device_data_bytes, device_data_union.device_data_bytes, sizeof(device_data_union.device_data_bytes));
    }
   
    // read serial
    while (Serial.available() > 0) {
      // read the incoming byte:
      char ch = Serial.read();
      
      recvBuffer[recvBufferIndex] = ch;
      // check packet complete
      if (recvBufferIndex > 2 && ch == '+' && recvBuffer[recvBufferIndex - 1] == '+' && recvBuffer[recvBufferIndex - 2] == '+') {
        serialType = recvBuffer[0];
        if (serialType == SERVO_SET_DATA && recvBufferIndex == sizeof(servo_set_data_union.servo_set_data_bytes) + 3) {
          // set servo angle
          for (byte n = 0; n < sizeof(servo_set_data_union.servo_set_data_bytes); n++) {
            servo_set_data_union.servo_set_data_bytes[n] = recvBuffer[n + 1];
          }
          // set servo angle
          Servo_180(servo_set_data_union.servo_set_data.index, servo_set_data_union.servo_set_data.angle);            
        } else 
        if (serialType == ADV_CONFIG_DATA) {
          if (recvBufferIndex == sizeof(adv_config_union.adv_config_bytes) + 3) {
            // update and save adv config data
            for (byte n = 0; n < sizeof(adv_config_union.adv_config_bytes); n++) {
              adv_config_union.adv_config_bytes[n] = recvBuffer[n + 1];
            }
            saveAdvConfig();   
          } else {
            // send adv config data
            Serial.write(ADV_CONFIG_DATA);
            Serial.write(adv_config_union.adv_config_bytes, sizeof(adv_config_union.adv_config_bytes));
            Serial.write('+');  // end sysbol
            Serial.write('+');  // end sysbol
            Serial.write('+');  // end sysbol  
          }          
        } else 
        if (serialType == HEARTBEAT_DATA) {
          last_heartbeat_time = millis();
        }
        serialType = SERIAL_NONE;
        recvBufferIndex = 0;
      } else {
        recvBufferIndex ++;
      }
      if (recvBufferIndex == sizeof(recvBuffer)) {
        recvBufferIndex = 0;
      }
    }

    check_heartbeat();
    // update prev read time
    sendTime = currTime;    
  }
}
