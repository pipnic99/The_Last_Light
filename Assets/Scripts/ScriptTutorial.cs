using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tutorial;
    public MovimientoJugador movimientoJugador;
    private bool textoactivo = false;
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        // Marcamos un condicionante para asegurarnos de que solo se active cuando el otro objeto tiene la etiqueta Player.
        if (other.CompareTag("Player"))
        {
            tutorial.gameObject.SetActive(true);
            movimientoJugador.haciendoAccion = true;
            textoactivo = true;
            Time.timeScale = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && textoactivo)
        {
            textoactivo = false;
            movimientoJugador.haciendoAccion = false;
            tutorial.gameObject.SetActive(false);
            Time.timeScale = 1;
            this.gameObject.SetActive(false);
        }
    }
}
