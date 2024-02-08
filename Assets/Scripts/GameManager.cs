using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool principioJuego = false;
    public CameraManager cameraManager;
    public MovimientoJugador movimientoJugador;
    public GameObject[] hudGameObjects;
    public GameObject[] playMenu;
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
        /*hudGameObjects = new GameObject[4];
        hudGameObjects[0] = GameObject.Find("Boton Accion");
        hudGameObjects[1] = GameObject.Find("EnterImage");
        hudGameObjects[2] = GameObject.Find("PressContinueText");
        hudGameObjects[3] = GameObject.Find("CantidadCuchillos");
        hudGameObjects[4] = GameObject.Find("ImageKnife");
        playMenu = new GameObject[1];
        playMenu[0] = GameObject.Find("ExitBttn");
        playMenu[1] = GameObject.Find("PlayBttn");*/
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
    public void Play()
    {
        foreach (GameObject hud in hudGameObjects)
        {
            hud.SetActive(true);
        }
        foreach (GameObject menu in playMenu)
        {
            menu.SetActive(false);
        }
        principioJuego = true;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
