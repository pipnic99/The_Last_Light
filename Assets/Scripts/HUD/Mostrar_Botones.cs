using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mostrar_Botones : MonoBehaviour
{
    public SubirEscaleras subirEscaleras;
    public AccionJugador accionJugador;
    public MovimientoJugador movimientoJugador;
    public Button_script button_Script;
    private Image imagen;
    public Sprite spriteF;
    public Sprite spriteE;
    public EscondertePuerta escondertePuerta;
    // Start is called before the first frame update
    void Start()
    {
        imagen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(escondertePuerta.puedesEscondertePuerta || subirEscaleras.puedesSubirEscaleras && !movimientoJugador.haciendoAccion)
        {
            this.gameObject.SetActive(true);
        }
        if(subirEscaleras.puedesSubirEscaleras || escondertePuerta.puedesEscondertePuerta || movimientoJugador.puedesBajarEsclareas && !movimientoJugador.haciendoAccion)
        {
            imagen.enabled = true;
            imagen.sprite = spriteF;
        }
        else if (accionJugador.puedesmatar && !movimientoJugador.haciendoAccion)
        {
            imagen.enabled = true;
            imagen.sprite = spriteE;
        }
        else if (button_Script.puedesPulsarBoton && !movimientoJugador.haciendoAccion)
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
