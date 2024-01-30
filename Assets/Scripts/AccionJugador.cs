using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionJugador : MonoBehaviour
{
    public CantidadCuchillos cantidadCuchillos;
    public bool matar = false;
    public bool laserActivo = true;
    public bool puedesmatar = false;
    public bool puedesPulsarBoton = false;
    private Transform transformBoton;
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
        if (puedesPulsarBoton && Input.GetKeyDown(KeyCode.F) && laserActivo)
        {
            // Ponemos el laser en falso y cambiamos el tag a laser apagado a los laseres que estan dentro del boton con el que estamos colisionando para saber que laseres tienen que estar apagados.
            laserActivo = false;
            foreach (Transform laser in transformBoton)
            {
                laser.gameObject.tag = "LaserApagado";
            }

        }
        else if (puedesPulsarBoton && Input.GetKeyDown(KeyCode.F) && !laserActivo)
        {
            laserActivo = true;
            foreach (Transform laser in transformBoton)
            {
                laser.gameObject.tag = "LaserEncendido";
            }
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
        if (collision.gameObject.CompareTag("BotonLaser"))
        {
            puedesPulsarBoton = true;
            transformBoton = collision.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Enemie"))
        {
            puedesmatar = false;
        }
        if (other.gameObject.CompareTag("BotonLaser"))
        {
            puedesPulsarBoton = false;
        }
    }

}
