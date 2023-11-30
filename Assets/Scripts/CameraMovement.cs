using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform personaje; // Referencia al transform del personaje que la c�mara debe seguir
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset para ajustar la posici�n de la c�mara
    public float suavizado = 5f; // Controla la velocidad de suavizado de la c�mara

    void Update()
    {
        if (personaje != null)
        {
            // Obtener la posici�n del personaje en 2D y agregar el offset
            Vector3 nuevaPosicion = new Vector3(personaje.position.x + offset.x, personaje.position.y + offset.y, offset.z);

            // Utilizar Lerp para suavizar el movimiento de la c�mara
            transform.position = Vector3.Lerp(transform.position, nuevaPosicion, suavizado * Time.deltaTime);
        }
    }
}