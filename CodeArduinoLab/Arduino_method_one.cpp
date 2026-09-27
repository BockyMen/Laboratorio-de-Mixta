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

  String mensaje = "";
  
  if (digitalRead(buttonPin4) == LOW) mensaje += "UP";
  if (digitalRead(buttonPin5) == LOW) mensaje += "RIGHT";
  if (digitalRead(buttonPin6) == LOW) mensaje += "LEFT";

  if (mensaje == "") mensaje = "STOP";

  int potValor = analogRead(A0); // Colocar bien el pin del potenciometro
  String velocidad = (potValor > 150) ? "FAST" : "SLOW";

  Serial.println(mensaje + ":" + velocidad); // UNA sola línea, ej: "UPRIGHT:FAST"
  Serial.flush();
  delay(50);
}