using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionJugador : MonoBehaviour
{
    public KillEnemie killEnemie;
    public CantidadCuchillos cantidadCuchillos;
    public Button_script scriptButton;
    public bool matar = false;
    public bool laserActivo = true;
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
        if (scriptButton.puedesPulsarBoton && Input.GetKeyDown(KeyCode.F) && laserActivo)
        {
            laserActivo = false;
        }
        else if (scriptButton.puedesPulsarBoton && Input.GetKeyDown(KeyCode.F) && !laserActivo)
        {
            laserActivo = true;
        }
    }
    
}
