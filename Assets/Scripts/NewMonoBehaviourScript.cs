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
                string[] partes = data.Split(':');
                string direccion = partes[0];
                string velocidad;
                if (partes.Length > 1){
                    velocidad = partes[1];
                }
                else{
                    velocidad = "SLOW";
                }
                float velocidadMovimiento = (velocidad == "FAST") ? 10f : 5f;

                if (direccion.Contains("UP")) transform.Translate(Vector3.up * Time.deltaTime * velocidadMovimiento);
                if (direccion.Contains("RIGHT")) transform.Translate(Vector3.right * Time.deltaTime * velocidadMovimiento);
                if (direccion.Contains("LEFT")) transform.Translate(Vector3.left * Time.deltaTime * velocidadMovimiento);

                if (direccion == "STOP") Debug.Log("STOP");
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
