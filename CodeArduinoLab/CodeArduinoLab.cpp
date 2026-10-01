/* 

// METHOD 1 - Delimiter framing

const int buttonPin4 = 4;
const int buttonPin5 = 5;
const int buttonPin6 = 6;

void setup() {
  Serial.begin(9600);

  pinMode(buttonPin4, INPUT);
  pinMode(buttonPin5, INPUT);
  pinMode(buttonPin6, INPUT);

  digitalWrite(buttonPin4, HIGH);
  digitalWrite(buttonPin5, HIGH);
  digitalWrite(buttonPin6, HIGH);
}

void loop() {

  String message = "";
  
  if (digitalRead(buttonPin4) == LOW) message += "UP";
  if (digitalRead(buttonPin5) == LOW) message += "RIGHT";
  if (digitalRead(buttonPin6) == LOW) message += "LEFT";

  if (message == "") message = "STOP";

  int potValue = analogRead(A0);
  String velocity = (potValue > 150) ? "FAST" : "SLOW";

  Serial.println(message + ":" + velocity);
  Serial.flush();
  delay(50);
}

*/

/*
// METHOD 2 - JSON representation

const int buttonPin4 = 4;
const int buttonPin5 = 5;
const int buttonPin6 = 6;

void setup() {
  Serial.begin(9600);
  pinMode(buttonPin4, INPUT_PULLUP);
  pinMode(buttonPin5, INPUT_PULLUP);
  pinMode(buttonPin6, INPUT_PULLUP);
}

void loop() {
  int up;
  if (digitalRead(buttonPin4) == LOW) {
    up = 1;
  } else {
    up = 0;
  }
  int right;
  if (digitalRead(buttonPin5) == LOW) {
    right = 1;
  } else {
    right = 0;
  }
  int left;
  if (digitalRead(buttonPin6) == LOW) {
    left = 1;
  } else {
    left = 0;
  }
  int pot;
  pot = analogRead(A0);

  Serial.print("{\"up\":");
  Serial.print(up);
  Serial.print(",\"right\":");
  Serial.print(right);
  Serial.print(",\"left\":");
  Serial.print(left);
  Serial.print(",\"pot\":");
  Serial.print(pot);
  Serial.println("}");

  Serial.flush();
  delay(50);
}

*/

//METHOD 3 - Raw binary, with header, length and integrity verification.

const int buttonPin4 = 4;
const int buttonPin5 = 5;
const int buttonPin6 = 6;

const byte header = 0xAA;

void setup() {
  Serial.begin(9600);
  pinMode(buttonPin4, INPUT_PULLUP);
  pinMode(buttonPin5, INPUT_PULLUP);
  pinMode(buttonPin6, INPUT_PULLUP);
}

void loop() {
  byte buttons = 0;
  if (digitalRead(buttonPin4) == LOW) buttons |= 0b001;
  if (digitalRead(buttonPin5) == LOW) buttons |= 0b010;
  if (digitalRead(buttonPin6) == LOW) buttons |= 0b100;

  int pot = analogRead(A0);
  byte potHigh = pot >> 8;
  byte potLow = pot & 0xFF;

  byte length = 3;
  byte checksum = length ^ buttons ^ potHigh ^ potLow;

  Serial.write(header);
  Serial.write(length);
  Serial.write(buttons);
  Serial.write(potHigh);
  Serial.write(potLow);
  Serial.write(checksum);

  Serial.flush();
  delay(50);
}