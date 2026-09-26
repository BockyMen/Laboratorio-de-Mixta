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
  
  if (digitalRead(buttonPin4) == LOW) {
    Serial.println("UP");
  }
  else if (digitalRead(buttonPin5) == LOW) {
    Serial.println("RIGHT");
  }
  else if (digitalRead(buttonPin6) == LOW) {
    Serial.println("LEFT");
  }
  else {
    Serial.println("STOP");
  }

  Serial.flush();
  delay(50);
}