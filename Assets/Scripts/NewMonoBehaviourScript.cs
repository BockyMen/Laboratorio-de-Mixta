using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

public class NewMonoBehaviourScript : MonoBehaviour
{
    SerialPort arduino = new SerialPort("COM3", 9600); //Colocar bien la entrada del arduino
    void Start()
{
    try
    {
        arduino.Open();
        arduino.ReadTimeout = 100; // sube esto, 1ms es demasiado corto
    }
    catch (System.Exception)
    {
    }
}

    void Update()
    {
        if (arduino.IsOpen)
        {
            try
            {
                string data = arduino.ReadLine().Trim();

                if (data == "UP")
                {
                    transform.Translate(Vector3.up * Time.deltaTime * 5);
                    Debug.Log("UP");
                }
                else if (data == "RIGHT")
                {
                    transform.Translate(Vector3.right * Time.deltaTime * 5);
                    Debug.Log("RIGHT");
                }
                else if (data == "LEFT")
                {
                    transform.Translate(Vector3.left * Time.deltaTime * 5);
                    Debug.Log("LEFT");
                }
                else if (data == "STOP")
                {
                    transform.Translate(Vector3.zero);
                    Debug.Log("STOP");
                }
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
