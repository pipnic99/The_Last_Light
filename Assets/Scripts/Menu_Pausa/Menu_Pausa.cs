using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Pausa : MonoBehaviour
{
    public GameObject objetoMenuPausa;
    public GameObject objetoOptions;
    public bool pausa = false;
    public GameManager gameManager;
    public GameObject objetoMenuDificultad;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Renaudar()
    {
            objetoMenuPausa.gameObject.SetActive(false);
            pausa = false;
            Time.timeScale = 1;
    }
    public void OptionsPauseMenu()
    {
        objetoMenuPausa.SetActive(false);
        objetoOptions.SetActive(true);
    }
    public void ExitOptionsPause()
    {
        objetoOptions.SetActive(false);
        objetoMenuPausa.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gameManager.canpause)
        {
            if(pausa == false)
            {
                objetoMenuPausa.gameObject.SetActive(true);
                pausa = true;
                Time.timeScale = 0;
            }
            else
            {
                objetoMenuPausa.gameObject.SetActive(false);
                objetoOptions.SetActive(false);
                objetoMenuPausa.SetActive(false);
                pausa = false;
                Time.timeScale = 1;
            }
        }
    }
}
