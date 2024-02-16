using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPuerta : MonoBehaviour
{
    public float velocidad = 2.0f;  // Puedes ajustar la velocidad según tus necesidades
    public GameManager gameManager;
    void Update()
    {
        // Verifica si se ha presionado la tecla Enter
        if (Input.GetKeyDown(KeyCode.Return) && gameManager.principioJuego)
        {
            // Llama a la función para iniciar el movimiento
            MoverObjetoEnX();
            enabled = false;
        }
    }

    void MoverObjetoEnX()
    {
        // Posición inicial
        Vector3 posicionInicial = transform.position;

        // Posición final desplazada en -1 en el eje X
        Vector3 posicionFinal = new Vector3(posicionInicial.x, posicionInicial.y , posicionInicial.z + 2.0f);

        // Inicia la interpolación hacia la posición final
        StartCoroutine(InterpolarPosicion(posicionInicial, posicionFinal, velocidad));
    }

    System.Collections.IEnumerator InterpolarPosicion(Vector3 inicio, Vector3 fin, float duracion)
    {
        float tiempo = 0f;

        while (tiempo < 1f)
        {
            tiempo += Time.deltaTime / duracion;
            transform.position = Vector3.Lerp(inicio, fin, tiempo);
            yield return null;
        }

        // Asegúrate de que la posición final sea exacta
        transform.position = fin;
    }
}
