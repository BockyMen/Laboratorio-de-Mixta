using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;   ///objeto que la camara va a seguir
    public float smoothness = 0.1f;  ////Velocidad de seguimiento
    public Vector3 distance; ////distancia entre la camara y jugador
    void LateUpdate()
    {
        Vector3 targetPosition = player.position + distance;
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, smoothness);
        transform.position = newPosition;
        
    }
}