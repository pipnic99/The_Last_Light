using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinEscalera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool finalEscalera = false;
    // Update is called once per frame

    // Ejecutamos la funcion de OnTriggerEnter para detectar cuando colisiona con nosotros un objeto con la etiqueta player.
    private void OnTriggerEnter(Collider other)
    {
        // Marcamos un condicionante para asegurarnos de que solo se active cuando el otro objeto tiene la etiqueta Player.
        if (other.CompareTag("Player"))
        {
            // Ponemos el valor de subirEscaleras a true.
            finalEscalera = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            finalEscalera = false;
        }
    }
}
