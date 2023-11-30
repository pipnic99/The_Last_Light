using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePickup : MonoBehaviour
{
    public CantidadCuchillos cantidadCuchillos;
    private void OnTriggerEnter(Collider other)
    {
        // Marcamos un condicionante para asegurarnos de que solo se active cuando el otro objeto tiene la etiqueta Player.
        if (other.CompareTag("Player"))
        {
            // Ponemos el valor de viendoJugador a true.
            cantidadCuchillos.numeroCuchillos += 1;
            Destroy(this.gameObject);
        }
    }
}
