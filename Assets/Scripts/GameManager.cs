using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool principioJuego = true;
    public CameraManager cameraManager;
    public MovimientoJugador movimientoJugador;
    // Start is called before the first frame update

    // Creamos una corutina para esperar 1s despues de pulsar enter y que nos deje mover pasado el segundo.
    IEnumerator Esperar1S()
    {
        // Esperar 1 segundo
        yield return new WaitForSeconds(2f);
        cameraManager.SwitchCamera(cameraManager.camaraPlayerFollow);
        yield return new WaitForSeconds(2f);
        // Hacer algo después de esperar
        movimientoJugador.haciendoAccion = false;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (principioJuego)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(Esperar1S());
            }
        }
    }
}
