using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

public class CodeBase : MonoBehaviour
{
    SerialPort arduino = new SerialPort("COM3", 9600);
    float lastMessageTime;
    bool lastPause;

    void Start()
    {
        try
        {
            arduino.Open();
            arduino.ReadTimeout = 100;
        }
        catch (System.TimeoutException)
        {
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    void Update()
    {
        for (int i = 0; i < 3 && arduino.IsOpen && arduino.BytesToRead > 0; i++)
        {
            try
            {
                string data = arduino.ReadLine().Trim();
                Debug.Log("Message: " + data);

                float ms = (Time.realtimeSinceStartup - lastMessageTime) * 1000f;
                lastMessageTime = Time.realtimeSinceStartup;

                string[] parts = data.Split(':');
                string direction = parts[0];
                string velocity;
                if (parts.Length > 1)
                    velocity = parts[1];
                else
                    velocity = "SLOW";
                float movementVelocity = (velocity == "FAST") ? 10f : 5f;

                if (direction.Contains("UP"))
                    transform.Translate(Vector3.up * Time.deltaTime * movementVelocity);

                if (direction.Contains("RIGHT"))
                    transform.Translate(Vector3.right * Time.deltaTime * movementVelocity);

                if (direction.Contains("LEFT"))
                    transform.Translate(Vector3.left * Time.deltaTime * movementVelocity);

                float msNoDelay = ms - 50f;

                bool pause = direction == "PAUSE";
                if (pause && !lastPause)
                    Debug.Log("PAUSE - time interval: " + msNoDelay.ToString("F1") + " ms");
                lastPause = pause;

                if (direction == "STOP")
                {
                }
            }
            catch (System.TimeoutException) { }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (arduino.IsOpen)
            arduino.Close();
    }
}