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

    bool up, right, left, pause, lastPause;
    int pot;

    int validPackages = 0;
    int corruptPackages = 0;

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
                    int length = arduino.ReadByte();
                    byte buttons = (byte)arduino.ReadByte();
                    byte potHigh = (byte)arduino.ReadByte();
                    byte potLow = (byte)arduino.ReadByte();
                    byte checksum = (byte)arduino.ReadByte();

                    byte calculed = (byte)(length ^ buttons ^ potHigh ^ potLow);

                    if (length == 3 && calculed == checksum)
                    {
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
                    }
                    else
                    {
                        corruptPackages++;
                        Debug.LogWarning("Invalid Package. Corrupt: " + corruptPackages);
                    }
                }
            }
        }
        catch (System.TimeoutException) { }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }

        float movementVelocity = velMin + ((pot - potMin) / (potMax - potMin)) * (velMax - velMin);

        if (up) transform.Translate(Vector3.up * Time.deltaTime * movementVelocity);
        if (right) transform.Translate(Vector3.right * Time.deltaTime * movementVelocity);
        if (left) transform.Translate(Vector3.left * Time.deltaTime * movementVelocity);
    }

    void OnApplicationQuit()
    {
        if (arduino.IsOpen)
            arduino.Close();
    }
}