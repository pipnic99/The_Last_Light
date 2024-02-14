using UnityEngine;

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
    public bool haciendoAccion = true;
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
    private bool yaGiradoIzquierda = false;
    private bool yaGiradoDerecha = true;
    bool movimientoAnteriorIzquierda = false;
    bool movimientoAnteriorDerecha = false;

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
    private void RotarDerecha()
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
            Debug.Log("Has tocado el laser");
        }
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
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        int movimientoHorizontalint = Mathf.RoundToInt(movimientoHorizontal);
        float movimientoVertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(movimientoHorizontal) > 0 && !haciendoAccion)
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

        if (movimientoHorizontal < 0f && !yaGiradoIzquierda && !movimientoAnteriorIzquierda)
        {
            RotarIzquierda();
            Debug.Log("giro izquierda");
            yaGiradoIzquierda = true;
            movimientoAnteriorIzquierda = true;
            movimientoAnteriorDerecha = false;
            yaGiradoDerecha = false;
        }
        else if (movimientoHorizontal > 0f && !yaGiradoDerecha && !movimientoAnteriorDerecha)
        {
            RotarDerecha();
            Debug.Log("giro derecha");
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
            if (Input.GetButtonDown("Jump") && !haciendoAccion)
            {
                movimiento.y = fuerzaSalto;
            }
        }
        else
        {
            movimiento.y -= gravedad * Time.deltaTime;
        }

        // Aplicar movimiento al CharacterController
        if (subirEscalera && Input.GetKeyDown(KeyCode.F) && !haciendoAccion)
        {
            upLadderPhase = 1;
            objetivoPosicion = transform.position + Vector3.forward * profundiadMovimientoJugador / 2;
            objetivoPosicion.z = PosicionEscalera.z - 1f;
            haciendoAccion = true;
        }
        if (puedesBajarEsclareas && Input.GetKeyDown(KeyCode.F) && !haciendoAccion)
        {
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
                objetivoPosicion = transform.position + Vector3.forward * (profundiadMovimientoJugador + 7f);
            }
            else if (escondidoEnPuerta)
            {
                objetivoPosicion = transform.position + Vector3.forward * (-profundiadMovimientoJugador - 7f);
            }
            haciendoAccion = true;
        }
        if (movimientoJugadorProfundiad)
        {

            if (!escondidoEnPuerta)
            {
                Vector3 movimientoProfundidad = Vector3.forward * velocidad * Time.deltaTime;
                if (transform.position.z < objetivoPosicion.z)
                {
                    characterController.Move(movimientoProfundidad);
                }
                else
                {
                    movimientoJugadorProfundiad = false;
                    escondidoEnPuerta = true;
                    haciendoAccion = false;
                }
            }
            else if (escondidoEnPuerta)
            {
                Vector3 movimientoProfundidad = Vector3.forward * (-velocidad) * Time.deltaTime;
                if (transform.position.z > objetivoPosicion.z)
                {
                    characterController.Move(movimientoProfundidad);
                }
                else
                {
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
                if (!finSubirEscalera)
                {
                    characterController.Move(movimientoVerticalJugador);
                }
                else
                {
                    upLadderPhase = 4;

                }
                break;
            case 4:
                if (!movimientoFinalEscalera3)
                {
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
                Vector3 movimientoVerticalJugador = Vector3.down * velocidad * Time.deltaTime;
                if (!finBajarEscalera)
                {
                    characterController.Move(movimientoVerticalJugador);
                }
                else
                {
                    downLadderPhase = 3;
                }
                break;
            case 3:
                if (!movimientoFinalEscalera3)
                {
                    objetivoPosicion = new Vector3(transform.position.x, transform.position.y, -1.23f); // Mover 2 unidades hacia adelante en el eje Z
                    movimientoFinalEscalera3 = true;
                }
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
}