using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform personaje; // Referencia al transform del personaje que la cámara debe seguir
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset para ajustar la posición de la cámara
    public float suavizado = 5f; // Controla la velocidad de suavizado de la cámara

    void Update()
    {
        if (personaje != null)
        {
            // Obtener la posición del personaje en 2D y agregar el offset
            Vector3 nuevaPosicion = new Vector3(personaje.position.x + offset.x, personaje.position.y + offset.y, offset.z);

            // Utilizar Lerp para suavizar el movimiento de la cámara
            transform.position = Vector3.Lerp(transform.position, nuevaPosicion, suavizado * Time.deltaTime);
        }
    }
}