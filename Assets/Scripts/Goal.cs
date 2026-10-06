using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject youWonPanel;

    void Start()
    {
        youWonPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            youWonPanel.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("Ganaste");
        }
    }
}