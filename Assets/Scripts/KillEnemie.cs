using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemie : MonoBehaviour
{
    public bool canKillEnemie = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // Marcamos un condicionante para asegurarnos de que solo se active cuando el otro objeto tiene la etiqueta Player.
        if (other.CompareTag("Player"))
        {
            // Ponemos el valor de viendoJugador a true.
            Debug.Log("PuedesMatar");
            canKillEnemie = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canKillEnemie = false;
        }
    }
}
