const int pingPin = 11;
const char BEARDUINO_COMMAND = 127;
const char POLL_RANGEFINDER = 122;
unsigned int duration, inches;
#include <Servo.h>

Servo myservo;
int val;

void setup() {
  myservo.attach(9);
  myservo.write(90);
  Serial.begin(9600);
}

void loop() {
  if(Serial.available())
  {
    char serialData[3];
    Serial.readBytesUntil(10, serialData, 3);
   
    if(serialData[0] == BEARDUINO_COMMAND) //Bearduino Command
    {
      int val = map((int)serialData[1], 26, 126, 0, 179);
      if(val > 179) val = 179;
      if(val < 0) val = 0;
      myservo.write(val);
    }
    if(serialData[0] == POLL_RANGEFINDER) //Return a value from ultrasonic rangefinder
    {
      pinMode(pingPin, OUTPUT);          // Set pin to OUTPUT
      digitalWrite(pingPin, LOW);        // Ensure pin is low
      delayMicroseconds(2);
      digitalWrite(pingPin, HIGH);       // Start ranging
      delayMicroseconds(5);              //   with 5 microsecond burst
      digitalWrite(pingPin, LOW);        // End ranging
      pinMode(pingPin, INPUT);           // Set pin to INPUT
      duration = pulseIn(pingPin, HIGH); // Read echo pulse
      inches = duration / 74 / 2;        // Convert to inches
      Serial.println(inches);            // Display result
      
    }
  }

}
