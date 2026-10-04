using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform jugador;   ///objeto que la camara va a seguir
    public float suavidad = 0.1f;  ////Velocidad de seguimiento
    public Vector3 distancia; ////distancia entre la camara y jugador
    void LateUpdate()
    {
        Vector3 positionObjetivo = jugador.position + distancia;
        Vector3 nuevaPosicion = Vector3.Lerp(transform.position, positionObjetivo, suavidad);
        transform.position = nuevaPosicion;
        
    }
}