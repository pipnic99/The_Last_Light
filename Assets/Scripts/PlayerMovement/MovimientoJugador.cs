using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovimientoJugador : MonoBehaviour
{
    //Importamos el character controlles
    public CharacterController characterController;
    private Animator animator;
    // Creamos las variables que necesitamos
    public float velocidad = 5f;
    public float fuerzaSalto = 8f;
    public float gravedad = 20f;
    public float distanciaSubir = 6f;
    public float profundiadMovimientoJugador = 1f;
    public DissolvingController dissolvingController;
    public bool haciendoAccion = true;
    public GameManager gameManager;
    // Creamos todas las booleanas que necesitaremos para las condiciones.
    private int upLadderPhase = 0;
    private int downLadderPhase = 0;
    private Vector3 objetivoPosicion;
    private bool movimientoFinalEscalera = false;
    private bool movimientoJugadorProfundiad = false;
    private bool escondidoEnPuerta = false;
    //private bool movimientoFinalEscalera2 = false;
    private bool movimientoFinalEscalera3 = false;
    private float posicionZJugador;
    public bool puedesBajarEsclareas = false;
    private bool finBajarEscalera = false;
    public bool puedesEscondertePuerta = false;
    private Vector3 posicionBajarEscalera;
    public int mostrar_boton = 0;
    private Vector3 PosicionEscalera = new Vector3 (0f,0f,0f);
    private bool finSubirEscalera = false;
    public bool subirEscalera = false;
    public int leftorright;
    public bool yaGiradoIzquierda = false;
    public bool yaGiradoDerecha = true;
    bool movimientoAnteriorIzquierda = false;
    bool movimientoAnteriorDerecha = false;
    public bool deathbylaser = false;
    public float movimientoHorizontal;
    private bool saltoUp = true;
    private Vector3 laserPosition;
    private AudioSource audioSource;

    public Vector3 movimiento = new Vector3(0f, 0f, 0f);
    private void RotarIzquierda()
    {
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
        }
    }
    public void RotarAccion(float rotacion)
    {
        if (yaGiradoIzquierda)
        {
            rotacion = rotacion * -1f;
        }
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
            transform.eulerAngles = Vector3.Lerp(nuevaRotacion, nuevaRotacion + new Vector3(0f, rotacion, 0f), tRotacion);
        }
    }
    public void RotarDerecha()
    {
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
            transform.eulerAngles = Vector3.Lerp(nuevaRotacion, nuevaRotacion + new Vector3(0f, -180f, 0f), tRotacion);
        }
    }
    void Start()
    {
        // Importamos el character controller.
        characterController = GetComponent<CharacterController>();
        posicionZJugador = transform.position.z;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("LadderDown"))
        {
            puedesBajarEsclareas = true;
            posicionBajarEscalera = collision.gameObject.transform.position;

        }
        if (collision.gameObject.CompareTag("FinSubirEscalera"))
        {
            finSubirEscalera = true;
        }
        if (collision.gameObject.CompareTag("FinBajarEscalera"))
        {
            finBajarEscalera = true;
        }
        if (collision.gameObject.CompareTag("EscondertePared"))
        {
            puedesEscondertePuerta = true;
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            subirEscalera = true;
            PosicionEscalera.z = collision.gameObject.transform.position.z;
        }
        if (collision.gameObject.CompareTag("LaserEncendido"))
        {
            deathbylaser = true;
            gameManager.IsAlive = false;
            laserPosition = collision.transform.position;
        }
    }
    IEnumerator CdSalto()
    {
        yield return new WaitForSeconds(2f);
        saltoUp = true;
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("LadderDown"))
        {
            puedesBajarEsclareas = false;
        }
        if (collision.gameObject.CompareTag("FinBajarEscalera"))
        {
            finBajarEscalera = false;
        }
        if (collision.gameObject.CompareTag("FinSubirEscalera"))
        {
            finSubirEscalera = false;
        }
        if (collision.gameObject.CompareTag("EscondertePared"))
        {
            puedesEscondertePuerta = false;
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            subirEscalera = false;
        }
    }
    void Update()
    {
        if (gameManager.IsAlive && animator.GetFloat("Stab") == 0)
        {
            movimientoHorizontal = Input.GetAxis("Horizontal");
            int movimientoHorizontalint = Mathf.RoundToInt(movimientoHorizontal);
            float movimientoVertical = Input.GetAxis("Vertical");

            if (Mathf.Abs(movimientoHorizontal) > 0 && !haciendoAccion )
            {
                animator.SetBool("IsWalking", true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }

            if (Mathf.Abs(movimientoHorizontal) == 0 && movimientoVertical < 0.5 && !haciendoAccion)
            {
                animator.SetBool("IsIdle", true);
            }
            else
            {
                animator.SetBool("IsIdle", false);
            }

            if (movimientoHorizontal < 0f && !yaGiradoIzquierda && !movimientoAnteriorIzquierda && !haciendoAccion)
            {
                RotarIzquierda();
                yaGiradoIzquierda = true;
                movimientoAnteriorIzquierda = true;
                movimientoAnteriorDerecha = false;
                yaGiradoDerecha = false;
            }
            else if (movimientoHorizontal > 0f && !yaGiradoDerecha && !movimientoAnteriorDerecha && !haciendoAccion)
            {
                RotarDerecha();
                yaGiradoDerecha = true;
                movimientoAnteriorDerecha = true;
                movimientoAnteriorIzquierda = false;
                yaGiradoIzquierda = false;
            }
            // Calcular movimiento del jugador
            Vector3 direccion = transform.right * movimientoVertical + transform.forward * Mathf.Abs(movimientoHorizontal);
            movimiento.x = direccion.x * velocidad;
            // Aplicar gravedad
            if (characterController.isGrounded)
            {
                movimiento.y = -gravedad * Time.deltaTime;

                // Manejar saltos
                if (Input.GetButtonDown("Jump") && !haciendoAccion && saltoUp)
                {
                    movimiento.y = fuerzaSalto;
                    animator.SetBool("IsJumping", true);
                    saltoUp = false;
                    StartCoroutine(CdSalto());
                }
            }
            else
            {
                movimiento.y -= gravedad * Time.deltaTime;
                animator.SetBool("IsJumping", false);
            }

            // Aplicar movimiento al CharacterController
            if (subirEscalera && Input.GetKeyDown(KeyCode.F) && !haciendoAccion)
            {
                upLadderPhase = 1;
                objetivoPosicion = transform.position + Vector3.forward * profundiadMovimientoJugador / 2;
                objetivoPosicion.z = PosicionEscalera.z - 1f;
                haciendoAccion = true;
                RotarAccion(-90f);
            }
            if (puedesBajarEsclareas && Input.GetKeyDown(KeyCode.F) && !haciendoAccion)
            {
                RotarAccion(-90f);
                downLadderPhase = 1;
                objetivoPosicion = transform.position + Vector3.forward * profundiadMovimientoJugador / 2;
                objetivoPosicion.z = posicionBajarEscalera.z - 1f;
                haciendoAccion = true;
            }
            if (puedesEscondertePuerta && Input.GetKeyDown(KeyCode.F) && !haciendoAccion)
            {

                movimientoJugadorProfundiad = true;
                if (!escondidoEnPuerta)
                {
                    RotarAccion(-90f);
                    objetivoPosicion = transform.position + Vector3.forward * (profundiadMovimientoJugador + 9f);
                }
                else if (escondidoEnPuerta)
                {
                    RotarAccion(90);
                    objetivoPosicion = transform.position + Vector3.forward * (-profundiadMovimientoJugador - 9f);
                }
                haciendoAccion = true;
            }
            if (movimientoJugadorProfundiad)
            {

                if (!escondidoEnPuerta)
                {
                    animator.SetBool("IsWalking", true);
                    velocidad = 10;
                    Vector3 movimientoProfundidad = Vector3.forward * velocidad * Time.deltaTime;
                    if (transform.position.z < objetivoPosicion.z)
                    {
                        characterController.Move(movimientoProfundidad);
                    }
                    else
                    {
                        RotarAccion(90f);
                        movimientoJugadorProfundiad = false;
                        escondidoEnPuerta = true;
                        haciendoAccion = false;
                    }
                }
                else if (escondidoEnPuerta)
                {
                    animator.SetBool("IsWalking", true);
                    Vector3 movimientoProfundidad = Vector3.forward * (-velocidad) * Time.deltaTime;
                    if (transform.position.z > objetivoPosicion.z)
                    {
                        characterController.Move(movimientoProfundidad);
                    }
                    else
                    {
                        velocidad = 5;
                        RotarAccion(-90f);
                        movimientoJugadorProfundiad = false;
                        escondidoEnPuerta = false;
                        haciendoAccion = false;
                    }
                }
            }


            //En este trozo de codigo calcularemos la mitad del recorrido deseado por nosotros y primero nos moveremos hacia la escalera,
            // luego subiremos por ella y cuando acabe nos moveremos hacia el fondo.

            switch (upLadderPhase)
            {
                case 1:
                    animator.SetBool("IsWalking", true);
                    Vector3 movimientoPrincipioProfundidad = Vector3.forward * velocidad * Time.deltaTime;
                    if (transform.position.z < objetivoPosicion.z && !movimientoFinalEscalera)
                    {
                        characterController.Move(movimientoPrincipioProfundidad);
                    }
                    else
                    {
                        upLadderPhase = 2;
                    }
                    break;
                case 2:
                    Vector3 movimientoVerticalJugador = Vector3.up * velocidad * Time.deltaTime;
                    animator.SetBool("SubirEscalera", true);
                    if (!finSubirEscalera)
                    {
                        characterController.Move(movimientoVerticalJugador);

                    }
                    else
                    {
                        upLadderPhase = 4;
                        animator.SetBool("SubirEscalera", false);

                    }
                    break;
                case 4:
                    if (!movimientoFinalEscalera3)
                    {
                        animator.SetBool("IsWalking", true);
                        RotarAccion(180f);
                        objetivoPosicion = new Vector3(transform.position.x, transform.position.y, -1.23f); // Mover 2 unidades hacia adelante en el eje Z
                        movimientoFinalEscalera3 = true;
                    }
                    Vector3 movimientoFinalProfundidad = Vector3.back * velocidad * Time.deltaTime;
                    if (transform.position.z > objetivoPosicion.z)
                    {
                        characterController.Move(movimientoFinalProfundidad);
                        if (transform.position.z < objetivoPosicion.z)
                        {
                            upLadderPhase = 5;
                        }
                    }
                    break;
                case 5:
                    animator.SetBool("IsIdle", true);
                    RotarAccion(-90f);
                    transform.position = new Vector3(transform.position.z, transform.position.y, -1.23f);
                    upLadderPhase = 0;
                    movimientoFinalEscalera = false;
                    //movimientoFinalEscalera2 = false;
                    movimientoFinalEscalera3 = false;
                    haciendoAccion = false;
                    break;
                default:
                    break;
            }

            switch (downLadderPhase)
            {
                case 1:
                    animator.SetBool("IsWalking", true);
                    Vector3 movimientoPrincipioProfundidad = Vector3.forward * velocidad * Time.deltaTime;
                    if (transform.position.z < objetivoPosicion.z && !movimientoFinalEscalera)
                    {
                        characterController.Move(movimientoPrincipioProfundidad);
                    }
                    else
                    {
                        downLadderPhase = 2;
                    }
                    break;
                case 2:
                    animator.SetBool("BajarEscalera", true);
                    Vector3 movimientoVerticalJugador = Vector3.down * velocidad * Time.deltaTime;
                    if (!finBajarEscalera)
                    {
                        characterController.Move(movimientoVerticalJugador);
                    }
                    else
                    {
                        downLadderPhase = 3;
                        animator.SetBool("BajarEscalera", false);
                    }
                    break;
                case 3:
                    if (!movimientoFinalEscalera3)
                    {
                        RotarAccion(180f);
                        objetivoPosicion = new Vector3(transform.position.x, transform.position.y, -1.23f); // Mover 2 unidades hacia adelante en el eje Z
                        movimientoFinalEscalera3 = true;
                    }
                    animator.SetBool("IsWalking", true);
                    Vector3 movimientoFinalProfundidad = Vector3.back * velocidad * Time.deltaTime;
                    if (transform.position.z > objetivoPosicion.z)
                    {
                        characterController.Move(movimientoFinalProfundidad);
                        if (transform.position.z < objetivoPosicion.z)
                        {
                            downLadderPhase = 4;
                        }
                    }
                    break;
                case 4:
                    RotarAccion(-90f);
                    transform.position = new Vector3(transform.position.z, transform.position.y, -1.23f);
                    downLadderPhase = 0;
                    movimientoFinalEscalera = false;
                    //movimientoFinalEscalera2 = false;
                    movimientoFinalEscalera3 = false;
                    haciendoAccion = false;
                    break;
                default:
                    break;
            }
            if (!haciendoAccion && !escondidoEnPuerta)
            {
                characterController.Move(movimiento * Time.deltaTime);
                if (transform.position.z != -1.23f && !escondidoEnPuerta)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, -1.23f);
                }
            }
        }
        else if(!gameManager.IsAlive && !deathbylaser)
        {
            animator.SetBool("IsDeath", true);
        }
        else if (!gameManager.IsAlive && deathbylaser)
        {
            animator.SetBool("LaserDeath", true);
            if(laserPosition.x + 5f > this.transform.position.x)
            {
                movimiento = new Vector3(-5, 0, 0);
                characterController.Move(movimiento * Time.deltaTime);
            }
            
        }
    }
}