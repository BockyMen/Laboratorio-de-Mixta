const int buttonPin4 = 4;
const int buttonPin5 = 5;
const int buttonPin6 = 6;
const int buttonPin7 = 7;

const byte header = 0xAA;

void setup() {
  Serial.begin(9600);
  pinMode(buttonPin4, INPUT_PULLUP);
  pinMode(buttonPin5, INPUT_PULLUP);
  pinMode(buttonPin6, INPUT_PULLUP);
  pinMode(buttonPin7, INPUT_PULLUP);
}

void loop() {
  byte buttons = 0;
  if (digitalRead(buttonPin4) == LOW) buttons |= 0b0001;
  if (digitalRead(buttonPin5) == LOW) buttons |= 0b0010;
  if (digitalRead(buttonPin6) == LOW) buttons |= 0b0100;
  if (digitalRead(buttonPin7) == LOW) buttons |= 0b1000;

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