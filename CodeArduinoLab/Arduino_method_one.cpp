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