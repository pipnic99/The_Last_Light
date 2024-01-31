using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion_enemigo_estatico : MonoBehaviour
{
    public float tiempoEspera = 2f; // Tiempo de espera en cada esquina del recorrido
    void Start()
    {
        StartCoroutine(RotarEnemigo());
    }

    // Ejecutamos la funcion coroutine que hemos declarado anteriormente.
    IEnumerator RotarEnemigo()
    {
        // Creamos un bucle infinito.
        while (true)
        {
            // Cuando el enemigo llega a su destino sale del bucle anterior y espera la mitad del tiempo deseado.
            yield return new WaitForSeconds(tiempoEspera / 2);
            // Gira 180 grados.
            // Creamos una variable para hacer un seguimiento de si esta completa la rotación.
            float elapsedTime = 0f;
            // Nos guardamos la escala actual por si hacemos alguna modificación poder volver a la escala original.
            Vector3 nuevaEscala = transform.localScale;
            // Guardamos temporalmente la rotacion de nuestro enemigo.
            Vector3 nuevaRotacion = transform.eulerAngles;
            // Creamos un bucle infinito mientras que la rotación no este completa.
            while (elapsedTime < 1f)
            {
                // Aumentamos el valor de elapsedTime en funcion del tiempo que ha transcurrido
                elapsedTime += Time.deltaTime;
                // Calcula un valor suavizado para la rotación
                float tRotacion = Mathf.SmoothStep(0, 1, elapsedTime / 1f);
                // Rotamos el enemigo por valor de 180º en la escala Y asignando el valor de suavizado que hemos creado en la linea anterior.
                transform.eulerAngles = Vector3.Lerp(nuevaRotacion, nuevaRotacion + new Vector3(0f, 180f, 0f), tRotacion);
                // Pasamos al siguiente frame.
                yield return null;
            }

            // Hacemos que el enemigo espere el valor introducido dividido entre 2.
            yield return new WaitForSeconds(tiempoEspera / 2);
        }
    }
}
