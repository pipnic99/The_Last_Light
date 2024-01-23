using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    //Importamos el character controlles
    public CharacterController characterController;
    // Importamos los scripts que usaremos.
    public SubirEscaleras subirEscaleras;
    public FinEscalera finEscalera;
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
    private bool movimientoFinalEscalera2 = false;
    private bool movimientoFinalEscalera3 = false;
    private float posicionZJugador;
    public bool puedesBajarEsclareas = false;
    private bool finBajarEscalera = false;
    public bool puedesEscondertePuerta = false;
    private Vector3 posicionBajarEscalera;
    public int mostrar_boton = 0;

    public Vector3 movimiento = new Vector3(0f, 0f, 0f);
    private void PulsarBoton()
    {

    }
    void Start()
    {
        // Importamos el character controller.
        characterController = GetComponent<CharacterController>();
        posicionZJugador = transform.position.z;
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("LadderDown"))
        {
            puedesBajarEsclareas = true;
            Debug.Log(collision.gameObject.transform.position);
            posicionBajarEscalera = collision.gameObject.transform.position;

        }
        if (collision.gameObject.CompareTag("FinBajarEscalera"))
        {
            finBajarEscalera = true;
        }
        if (collision.gameObject.CompareTag("EscondertePared"))
        {
            puedesEscondertePuerta = true;
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
        if (collision.gameObject.CompareTag("EscondertePared"))
        {
            puedesEscondertePuerta = false;
        }
    }
    void Update()
    {
        // Obtener entrada del jugador
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcular movimiento del jugador
        Vector3 direccion = transform.right * movimientoHorizontal + transform.forward * movimientoVertical;
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
        if (subirEscaleras.puedesSubirEscaleras && Input.GetKeyDown(KeyCode.F) && !haciendoAccion)
        {
            upLadderPhase = 1;
            objetivoPosicion = transform.position + Vector3.forward * profundiadMovimientoJugador / 2;
            objetivoPosicion.z = subirEscaleras.transform.position.z - 1f;
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
                if (!finEscalera.finalEscalera)
                {
                    characterController.Move(movimientoVerticalJugador);
                }
                else
                {
                    upLadderPhase = 3;

                }
                break;
            case 3:
                if (!movimientoFinalEscalera2)
                {
                    if (subirEscaleras.escaleraIzquierda)
                    {
                        objetivoPosicion = transform.position + Vector3.left * profundiadMovimientoJugador / 2; // Mover 2 unidades hacia adelante en el eje Z
                        movimientoFinalEscalera2 = true;
                    }
                    else if (!subirEscaleras.escaleraIzquierda)
                    {
                        objetivoPosicion = transform.position + Vector3.right * profundiadMovimientoJugador / 2; // Mover 2 unidades hacia adelante en el eje Z
                        movimientoFinalEscalera2 = true;
                    }

                }
                //Vector3 movimientoProfundidad = Vector3.right * velocidad * Time.deltaTime;
                if (transform.position.x < objetivoPosicion.x && !subirEscaleras.escaleraIzquierda)
                {
                    Vector3 movimientoProfundidad = Vector3.right * velocidad * Time.deltaTime;
                    characterController.Move(movimientoProfundidad);
                }
                else if (transform.position.x > objetivoPosicion.x && subirEscaleras.escaleraIzquierda)
                {
                    Vector3 movimientoProfundidad = Vector3.right * -velocidad * Time.deltaTime;
                    characterController.Move(movimientoProfundidad);
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

                Debug.Log(movimiento);
                transform.position = new Vector3(transform.position.z, transform.position.y, -1.23f);
                upLadderPhase = 0;
                movimientoFinalEscalera = false;
                movimientoFinalEscalera2 = false;
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

                Debug.Log(movimiento);
                transform.position = new Vector3(transform.position.z, transform.position.y, -1.23f);
                downLadderPhase = 0;
                movimientoFinalEscalera = false;
                movimientoFinalEscalera2 = false;
                movimientoFinalEscalera3 = false;
                haciendoAccion = false;
                break;
            default:
                break;
        }
        if (!haciendoAccion)
        {
            characterController.Move(movimiento * Time.deltaTime);
            if (transform.position.z != -1.23f && !escondidoEnPuerta)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -1.23f);
            }
        }
    }
}