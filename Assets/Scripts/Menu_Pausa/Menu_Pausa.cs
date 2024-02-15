using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Pausa : MonoBehaviour
{
    public GameObject objetoMenuPausa;
    public bool pausa = false;
    public GameManager gameManager;
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
                pausa = false;
                Time.timeScale = 1;
            }
        }
    }
}
