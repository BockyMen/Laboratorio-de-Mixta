using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

public class CodeBase : MonoBehaviour
{
    SerialPort arduino = new SerialPort("COM3", 9600);
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
        if (arduino.IsOpen)
        {
            try
            {
                string data = arduino.ReadLine().Trim();
                string[] parts = data.Split(':');
                string direction = parts[0];
                string velocity;
                if (parts.Length > 1){
                    velocity = parts[1];
                }
                else{
                    velocity = "SLOW";
                }
                float movementVelocity = (velocity == "FAST") ? 10f : 5f;

                if (direction.Contains("UP")) transform.Translate(Vector3.up * Time.deltaTime * movementVelocity);
                if (direction.Contains("RIGHT")) transform.Translate(Vector3.right * Time.deltaTime * movementVelocity);
                if (direction.Contains("LEFT")) transform.Translate(Vector3.left * Time.deltaTime * movementVelocity);

                if (direction == "STOP") Debug.Log("STOP");
            }
            catch (System.TimeoutException)
            {
            }
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
