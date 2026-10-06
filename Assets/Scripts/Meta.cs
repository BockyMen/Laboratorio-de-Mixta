using UnityEngine;

public class Meta : MonoBehaviour
{
    public GameObject panelGanaste;

    void Start()
    {
        panelGanaste.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            panelGanaste.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("Ganaste");
        }
    }
}