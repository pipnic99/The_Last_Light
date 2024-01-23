using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mostrar_Botones : MonoBehaviour
{
    public SubirEscaleras subirEscaleras;
    public AccionJugador accionJugador;
    public MovimientoJugador movimientoJugador;
    private Image imagen;
    public Sprite spriteF;
    public Sprite spriteE;
    public CantidadCuchillos cantidadCuchillos;
    // Start is called before the first frame update
    void Start()
    {
        imagen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movimientoJugador.puedesEscondertePuerta || subirEscaleras.puedesSubirEscaleras && !movimientoJugador.haciendoAccion)
        {
            this.gameObject.SetActive(true);
        }
        if(subirEscaleras.puedesSubirEscaleras || movimientoJugador.puedesEscondertePuerta || movimientoJugador.puedesBajarEsclareas && !movimientoJugador.haciendoAccion)
        {
            imagen.enabled = true;
            imagen.sprite = spriteF;
        }
        else if (accionJugador.puedesmatar && !movimientoJugador.haciendoAccion && cantidadCuchillos.numeroCuchillos != 0)
        {
            imagen.enabled = true;
            imagen.sprite = spriteE;
        }
        else if (accionJugador.puedesPulsarBoton && !movimientoJugador.haciendoAccion)
        {
            imagen.enabled = true;
            imagen.sprite = spriteF;
        }
        else if(movimientoJugador.haciendoAccion)
        {
            imagen.enabled = false;
        }
        else
        {
            imagen.enabled = false;
        }
    }
}
