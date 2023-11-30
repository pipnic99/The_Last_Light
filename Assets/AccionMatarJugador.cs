using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionMatarJugador : MonoBehaviour
{
    public KillEnemie killEnemie;
    public CantidadCuchillos cantidadCuchillos;
    public bool matar = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (killEnemie.canKillEnemie && Input.GetKeyDown(KeyCode.E) && cantidadCuchillos.numeroCuchillos > 0)
        {
            matar = true;
            cantidadCuchillos.numeroCuchillos -= 1;
        }
    }
    
}
