using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slide;
    public float damage;
    public event EventHandler Death;      
    public GameObject youFinished;
    void Start()
    {
        youFinished.SetActive(false);
    }
    void Update()
    {
        if (slide.value <= 0)
        {   
            
            youFinished.SetActive(true);

            Time.timeScale = 0f;

            Debug.Log("Game Over");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Death?.Invoke(this, EventArgs.Empty);

            Debug.Log("Entre");

            slide.value -= damage;
            other.GetComponent<Animator>().SetTrigger("Hurt");
        }
    }
}
