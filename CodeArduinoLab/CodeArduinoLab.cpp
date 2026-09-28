/* 

// METODO 1 - Framing por delimitador

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

*/

/*
// METODO 2 - Representacion en JSON

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
  pot = analogRead(A0); // Colocar bien el pin del potenciometro

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

//METODO 3 - Binario crudo, con encabezado, longitud y verificacion de integridad.

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