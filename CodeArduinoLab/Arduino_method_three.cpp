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
  byte botones = 0;
  if (digitalRead(buttonPin4) == LOW) botones |= 0b001;
  if (digitalRead(buttonPin5) == LOW) botones |= 0b010;
  if (digitalRead(buttonPin6) == LOW) botones |= 0b100;

  int pot = analogRead(A0);
  byte potAlto = pot >> 8;
  byte potBajo = pot & 0xFF;

  byte longitud = 3;
  byte checksum = longitud ^ botones ^ potAlto ^ potBajo;

  Serial.write(header);
  Serial.write(longitud);
  Serial.write(botones);
  Serial.write(potAlto);
  Serial.write(potBajo);
  Serial.write(checksum);

  Serial.flush();
  delay(50);
}