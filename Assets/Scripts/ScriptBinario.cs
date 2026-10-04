using UnityEngine;
using System.IO.Ports;

public class ScriptBinario : MonoBehaviour
{
    SerialPort arduino = new SerialPort("COM3", 9600);

    const byte header = 0xAA;
    const int length_package = 6;

    float potMin = 0f;
    float potMax = 1023f;
    float velMin = 2f;
    float velMax = 15f;

<<<<<<< Updated upstream
    bool up, right, left;
=======
    float ultimoPaquete;

    bool up, right, left, pause, lastPause;
>>>>>>> Stashed changes
    int pot;

    int paquetesValidos = 0;
    int paquetesCorruptos = 0;

    void Start()
    {
        try
        {
            arduino.Open();
            arduino.ReadTimeout = 100;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    void Update()
    {
        if (!arduino.IsOpen)
        {
            return;
        }
        try
        {
            while (arduino.BytesToRead >= length_package)
            {
                if (arduino.ReadByte() == header)
                {
                    int longitud = arduino.ReadByte();
                    byte botones = (byte)arduino.ReadByte();
                    byte potAlto = (byte)arduino.ReadByte();
                    byte potBajo = (byte)arduino.ReadByte();
                    byte checksum = (byte)arduino.ReadByte();

                    byte calculado = (byte)(longitud ^ botones ^ potAlto ^ potBajo);

                    if (longitud == 3 && calculado == checksum)
                    {
<<<<<<< Updated upstream
                        up = (botones & 0b001) != 0;
                        right = (botones & 0b010) != 0;
                        left = (botones & 0b100) != 0;
                        pot = (potAlto << 8) | potBajo;
                        paquetesValidos++;
=======
                        up = (buttons & 0b001) != 0;
                        right = (buttons & 0b010) != 0;
                        left = (buttons & 0b100) != 0;
                        pause = (buttons & 0b1000) != 0;
                        pot = (potHigh << 8) | potLow;
                        validPackages++;

                        if (pause && !lastPause)
                        {
                            
                            
                            Debug.Log("pause");
                        }
                        lastPause = pause;
>>>>>>> Stashed changes
                    }
                    else
                    {
                        paquetesCorruptos++;
                        Debug.LogWarning("Paquete descartado. Corruptos: " + paquetesCorruptos);
                    }
                }
            }
        }
        catch (System.TimeoutException) { }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }

        float velocidadMovimiento = velMin + ((pot - potMin) / (potMax - potMin)) * (velMax - velMin);

        if (up) transform.Translate(Vector3.up * Time.deltaTime * velocidadMovimiento);
        if (right) transform.Translate(Vector3.right * Time.deltaTime * velocidadMovimiento);
        if (left) transform.Translate(Vector3.left * Time.deltaTime * velocidadMovimiento);
    }

    void OnApplicationQuit()
    {
        if (arduino.IsOpen)
            arduino.Close();
    }
}