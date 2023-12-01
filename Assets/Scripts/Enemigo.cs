using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Importamos el script deteccionJugador.
    public DeteccionJugador deteccionJugador;
    public AccionJugador accionMatarJugador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Creamos una condicion que si la variable viendoJugador == True, entramos y ejecutamos la animación.
        if (deteccionJugador.viendoJugador == true)
        {
            Debug.Log("Animacion");
            // Asignamos el valor de viendoJugador a false.
            deteccionJugador.viendoJugador = false;
        }
        if (accionMatarJugador.matar)
        {
            Debug.Log("Animación matar");
            Destroy(this.gameObject);
            accionMatarJugador.matar = false;
        }
    }
}
