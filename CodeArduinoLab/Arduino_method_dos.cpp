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