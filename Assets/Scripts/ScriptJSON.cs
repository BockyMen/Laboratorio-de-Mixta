using UnityEngine;
using System.IO.Ports;

public class ScriptJSON : MonoBehaviour
{
    [System.Serializable]
    public class ArduinoData
    {
        public int up;
        public int right;
        public int left;
        public int pot;
    }

    SerialPort arduino = new SerialPort("COM3", 9600);

    float potMin = 0f;
    float potMax = 1023f;
    float velMin = 2f;
    float velMax = 15f;

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
                ArduinoData estado = JsonUtility.FromJson<ArduinoData>(data);

                float velocidadMovimiento = velMin + ((estado.pot - potMin) / (potMax - potMin)) * (velMax - velMin);

                if (estado.up == 1)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * velocidadMovimiento);
                }
                if (estado.right == 1)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * velocidadMovimiento);
                }
                if (estado.left == 1)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * velocidadMovimiento);
                }
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