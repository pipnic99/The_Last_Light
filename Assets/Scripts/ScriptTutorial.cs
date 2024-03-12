using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTutorial : MonoBehaviour
{
    public GameObject[] tutorial;
    public MovimientoJugador movimientoJugador;
    [SerializeField] GameManager gameManager;
    private int tutorialActivo = -1;
    private bool mostrandoTutorial = false;

    void Start()
    {
        DesactivarTodosLosTutoriales();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tutorialActivo == -1)
        {
            MostrarSiguienteTutorial();
        }
    }

    void Update()
    {
        if (mostrandoTutorial && Input.GetKeyDown(KeyCode.Return))
        {
            if (tutorialActivo < tutorial.Length - 1)
            {
                MostrarSiguienteTutorial();
            }
            else
            {
                FinalizarTutorial();
            }
        }
    }

    void MostrarSiguienteTutorial()
    {
        if (tutorialActivo >= 0)
        {
            tutorial[tutorialActivo].SetActive(false);
        }

        tutorialActivo++;
        tutorial[tutorialActivo].SetActive(true);
        mostrandoTutorial = true;
        movimientoJugador.haciendoAccion = true;
        gameManager.canpause = false;
        Time.timeScale = 0;
    }

    void DesactivarTodosLosTutoriales()
    {
        foreach (GameObject tutorialObj in tutorial)
        {
            tutorialObj.SetActive(false);
        }
    }

    void FinalizarTutorial()
    {
        tutorial[tutorialActivo].SetActive(false);
        mostrandoTutorial = false;
        movimientoJugador.haciendoAccion = false;
        gameManager.canpause = true;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
