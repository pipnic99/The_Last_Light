using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoParpadeante : MonoBehaviour
{
    public float velocidadDesvanecimiento = 1f; // Velocidad de desvanecimiento
    private Text texto; // Referencia al componente Text
    private bool isPaused = false; // Estado de pausa

    private void OnEnable()
    {
        texto = GetComponent<Text>(); // Obtener el componente Text
        // Iniciar la corrutina para el desvanecimiento
        StartCoroutine(Desvanecer());
    }

    private IEnumerator Desvanecer()
    {
        while (true)
        {
            // Desvanecer gradualmente el texto
            while (texto.color.a > 0)
            {
                if (!isPaused)
                {
                    Color colorActual = texto.color;
                    colorActual.a -= Time.unscaledDeltaTime * velocidadDesvanecimiento;
                    texto.color = colorActual;
                }
                yield return null;
            }

            // Asegurarse de que el texto esté completamente invisible
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 0);

            // Esperar un momento antes de hacer que el texto vuelva a aparecer
            yield return new WaitForSecondsRealtime(1f);

            // Volver a hacer que el texto aparezca gradualmente
            while (texto.color.a < 1)
            {
                if (!isPaused)
                {
                    Color colorActual = texto.color;
                    colorActual.a += Time.unscaledDeltaTime * velocidadDesvanecimiento;
                    texto.color = colorActual;
                }
                yield return null;
            }

            // Asegurarse de que el texto esté completamente visible
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 1);

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
