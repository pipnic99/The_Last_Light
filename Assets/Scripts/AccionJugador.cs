using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionJugador : MonoBehaviour
{
    public CantidadCuchillos cantidadCuchillos;
    public Button_script scriptButton;
    public bool matar = false;
    public bool laserActivo = true;
    public bool puedesmatar = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cantidadCuchillos.numeroCuchillos > 0)
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
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemie"))
        {
            puedesmatar = true;
            if (matar)
            {
                collision.gameObject.SetActive(false);
                puedesmatar = false;
                matar = false;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Enemie"))
        {
            puedesmatar = false;
            Debug.Log(puedesmatar);
        }
    }

}
