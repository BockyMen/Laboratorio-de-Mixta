using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarraDeVida : MonoBehaviour
{
    public Slider slide;
    public float daño;
    public event EventHandler Muerte;      
    public GameObject terminaste;
    void Start()
    {
        terminaste.SetActive(false);
    }
    void Update()
    {
        if (slide.value <= 0)
        {   
            
            terminaste.SetActive(true);

            Time.timeScale = 0f;

            Debug.Log("Game Over");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Muerte?.Invoke(this, EventArgs.Empty);

            Debug.Log("Entre");

            slide.value -= daño;
            other.GetComponent<Animator>().SetTrigger("Hurt");
        }
    }
}
