using UnityEngine;
using System.Collections;

public class MovimientoEnemigo : MonoBehaviour
{
    public float velocidad = 2f;  // Velocidad de movimiento del enemigo
    public float distancia = 5f;  // Distancia que recorrerá el enemigo
    public float tiempoEspera = 2f; // Tiempo de espera en cada esquina del recorrido
    public GameManager gameManager;
    public Animator animator;

    private Vector3 puntoInicial;  // Punto de inicio del movimiento
    private Vector3 puntoFinal;    // Punto final del movimiento

    void Start()
    {
        // Guardamos la posicion inicial del enemigo en el vector 3 que hemos creado.
        puntoInicial = transform.position;
        // Guardamos en punto final la posicion de punto inicial + la distancia deseada hacia la direccion deseada.
        puntoFinal = puntoInicial + Vector3.right * distancia;
        // Iniciamos un metodo coroutine para poder pausarlo y retomarlo a nuestro gusto.
        StartCoroutine(MoverEnemigo());
        animator = GetComponent<Animator>();
    }

    // Ejecutamos la funcion coroutine que hemos declarado anteriormente.
    IEnumerator MoverEnemigo()
    {
        // Creamos un bucle infinito.
        while (gameManager.IsAlive)
        {
            animator.SetBool("IsWalking", true);
            // Mueve al enemigo de un lado a otro en un bucle infinito.
            // Creamos un float denominado t para realizar un seguimiento del progreso del movimiento entre los puntos inicial y final.
            float t = 0f;
            // Creamos un bucle que se ejecute mientras que el objetivo no ha llegado a su destino.
            while (t < 1f && gameManager.IsAlive)
            {
                // Aumentamos el valor t basandonos en el tiempo transcurrido, esto lo hacemos para suavizar el movimiento.
                t += Time.deltaTime * velocidad / distancia;
                //Movemos el enemigo gradualmente entre el punto inicial y el punto final usando la funcion Lerp
                transform.position = Vector3.Lerp(puntoInicial, puntoFinal, t);
                // Pasa la funcion coroutine hasta el proximo frame.
                yield return null;
            }
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsWalking", false);
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
            while (elapsedTime < 1f && gameManager.IsAlive)
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
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdle", false);
            // Cambiamos las direcciones
            // Creamos un vector3 y guardamos el valor de punto inicial
            Vector3 temp = puntoInicial;
            // Cogemos el valor de punto final y lo pasamo a punto inicial
            puntoInicial = puntoFinal;
            // Cogemos el valor temporal que hemos creado y lo pasamos a punto final.
            puntoFinal = temp;
        }
    }
}