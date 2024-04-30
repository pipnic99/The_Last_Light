using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool principioJuego = false;
    public CameraManager cameraManager;
    public MovimientoJugador movimientoJugador;
    public GameObject[] hudGameObjects;
    public GameObject[] playMenu;
    public GameObject optionMenu;
    public bool canpause = false;
    public bool IsAlive = true;
    public AudioSource audioSource;
    public bool blood = false;
    public GameObject fleshImpact;
    private bool done = false;
    public GameObject objetoMenuDificultad;
    public int difficulty = 0;
    public GameObject[] NormalKnives;
    public GameObject[] EasyKnives;
    public CantidadCuchillos cantidadCuchillos;
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
        canpause = true;
    }
    IEnumerator EsperarMuerte()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Game_Scene");
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
    public void SetDifficulty(string diff)
    {
        if(diff == "Easy")
        {
            cantidadCuchillos.numeroCuchillos = 4;
        }
        else if(diff == "Normal")
        {
            cantidadCuchillos.numeroCuchillos = 2;
        }
        else if(diff == "ChrisVector")
        {
            cantidadCuchillos.numeroCuchillos = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (principioJuego)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(Esperar1S());
                hudGameObjects[4].SetActive(false);
            }
        }
        if(!IsAlive)
        {
            if (!done && blood == true)
            {
                fleshImpact.SetActive(true);
                audioSource.Play();
                done = true;
            }
            StartCoroutine(EsperarMuerte());
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
    public void OptionsMainMenu()
    {
        optionMenu.SetActive(true);
        foreach (GameObject menu in playMenu)
        {
            menu.SetActive(false);
        }
    }
    public void EnterDifficulty()
    {
        objetoMenuDificultad.SetActive(true);
        optionMenu.SetActive(false);
    }
    public void ExitDifficulty()
    {
        objetoMenuDificultad.SetActive(false);
        optionMenu.SetActive(true);
    }
    public void BackMenu()
    {
        foreach (GameObject menu in playMenu)
        {
            menu.SetActive(true);
        }
        optionMenu.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
