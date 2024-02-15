using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagenParpadeante : MonoBehaviour
{
    public float velocidadDesvanecimiento = 1f; // Velocidad de desvanecimiento
    private Image imagen; // Referencia al componente Image
    private bool isPaused = false; // Estado de pausa

    private void OnEnable()
    {
        imagen = GetComponent<Image>(); // Obtener el componente Image
        // Iniciar la corrutina para el desvanecimiento
        StartCoroutine(Desvanecer());
    }

    private IEnumerator Desvanecer()
    {
        while (true)
        {
            // Desvanecer gradualmente la imagen
            while (imagen.color.a > 0)
            {
                if (!isPaused)
                {
                    Color colorActual = imagen.color;
                    colorActual.a -= Time.unscaledDeltaTime * velocidadDesvanecimiento;
                    imagen.color = colorActual;
                }
                yield return null;
            }

            // Asegurarse de que la imagen esté completamente invisible
            imagen.color = new Color(imagen.color.r, imagen.color.g, imagen.color.b, 0);

            // Esperar un momento antes de hacer que la imagen vuelva a aparecer
            yield return new WaitForSecondsRealtime(1f);

            // Volver a hacer que la imagen aparezca gradualmente
            while (imagen.color.a < 1)
            {
                if (!isPaused)
                {
                    Color colorActual = imagen.color;
                    colorActual.a += Time.unscaledDeltaTime * velocidadDesvanecimiento;
                    imagen.color = colorActual;
                }
                yield return null;
            }

            // Asegurarse de que la imagen esté completamente visible
            imagen.color = new Color(imagen.color.r, imagen.color.g, imagen.color.b, 1);

            // Esperar un momento antes de iniciar el siguiente ciclo de desvanecimiento
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    // Método para establecer el estado de pausa
    public void SetPaused(bool paused)
    {
        isPaused = paused;
    }
}
