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
        public int pause;
        public int pot;
    }

    SerialPort arduino = new SerialPort("COM3", 9600);

    float potMin = 0f;
    float potMax = 1023f;
    float velMin = 2f;
    float velMax = 15f;

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
        if (arduino.IsOpen)
        {
            try
            {
                string data = arduino.ReadLine().Trim();
                ArduinoData state = JsonUtility.FromJson<ArduinoData>(data);

                float ms = (Time.realtimeSinceStartup - lastMessageTime) * 1000f;
                lastMessageTime = Time.realtimeSinceStartup;

                float movementVelocity = velMin + ((state.pot - potMin) / (potMax - potMin)) * (velMax - velMin);

                if (state.up == 1)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * movementVelocity);
                }
                if (state.right == 1)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * movementVelocity);
                }
                if (state.left == 1)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * movementVelocity);
                }

                float msNoDelay = ms - 50f;
                bool pause = state.pause == 1;
                if (pause && !lastPause)
                {
                    Debug.Log("PAUSE - time interval: " + msNoDelay.ToString("F1") + " ms");
                }
                lastPause = pause;
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