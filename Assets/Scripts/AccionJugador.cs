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
    private bool done = false;
    private bool pushingButtton = false;
    private bool rotated = false;
    private GameObject actualEnemy;
    private MovimientoEnemigo movimientoEnemigo;
    private Rotacion_enemigo_estatico rotacion_Enemigo_Estatico;
    private RaycastDetection raycastDetection;
    private bool shutdownlaser = false;
    private bool turnuplaser = false;
    private Transform transformBoton;
    public Animator animator;
    public AudioSource stab;
    public MovimientoJugador movimientoJugador;
    // Start is called before the first frame update
    void Start()
    {

    }
    IEnumerator Waitfor10Seconds(GameObject enemigo)
    {
        yield return new WaitForSeconds(10);
        enemigo.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if(rotated && animator.GetFloat("EndButton") == 1 && pushingButtton)
        {
            if(movimientoJugador.yaGiradoDerecha)
            {
                movimientoJugador.RotarAccion(90);
            }
            else
            {
                movimientoJugador.RotarAccion(90);
            }
            rotated = false;
            pushingButtton = false;
            movimientoJugador.haciendoAccion = false;
        }
        if(animator.GetFloat("Stab") == 1 && !done)
        {
            stab.Play();
            done = true;
        }
        else if(animator.GetFloat("Stab") == 0)
        {
            done = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && cantidadCuchillos.numeroCuchillos > 0)
        {
            matar = true;
            cantidadCuchillos.numeroCuchillos -= 1;
        }
        if (puedesPulsarBoton && Input.GetKeyDown(KeyCode.F) && laserActivo && !pushingButtton)
        {
            // Ponemos el laser en falso y cambiamos el tag a laser apagado a los laseres que estan dentro del boton con el que estamos colisionando para saber que laseres tienen que estar apagados.
            laserActivo = false;
            animator.SetTrigger("ButtonClick");
            movimientoJugador.RotarAccion(-90);
            rotated = true;
            pushingButtton = true;
            movimientoJugador.haciendoAccion = true;
            shutdownlaser = true;
        }
        if(shutdownlaser && animator.GetFloat("PushButton") == 1)
        {
            AudioSource sonidoBoton = transformBoton.GetComponent<AudioSource>();
            sonidoBoton.Play();
            foreach (Transform laser in transformBoton)
            {
                if (laser.name == "OnOffSound")
                {
                    AudioSource sonidoLaser;
                    sonidoLaser = laser.GetComponent<AudioSource>();
                    sonidoLaser.Play();
                }
                if (laser.name != "LuzBoton")
                {
                    laser.gameObject.tag = "LaserApagado";
                }
                else if (laser.name == "LuzBoton")
                {
                    laser.gameObject.SetActive(false);
                }
            }
            shutdownlaser = false;
        }
        if(turnuplaser && animator.GetFloat("PushButton") == 1)
        {
            AudioSource sonidoBoton = transformBoton.GetComponent<AudioSource>();
            sonidoBoton.Play();
            foreach (Transform laser in transformBoton)
            {
                Debug.Log(laser.name);
                if (laser.name != "LuzBoton")
                {
                    laser.gameObject.tag = "LaserEncendido";
                }
                else if (laser.name == "LuzBoton")
                {
                    laser.gameObject.SetActive(true);
                }
            }
            turnuplaser = false;
        }
        else if (puedesPulsarBoton && Input.GetKeyDown(KeyCode.F) && !laserActivo && !pushingButtton)
        {
            laserActivo = true;
            animator.SetTrigger("ButtonClick");
            movimientoJugador.RotarAccion(-90);
            rotated = true;
            movimientoJugador.haciendoAccion = true;
            pushingButtton = true;
            turnuplaser = true;
            
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemie"))
        {
            puedesmatar = true;
            if (matar)
            {
                if(collision.gameObject.GetComponent<MovimientoEnemigo>() != null)
                {
                    movimientoEnemigo = collision.gameObject.GetComponent<MovimientoEnemigo>();
                    movimientoEnemigo.enemyDead = true;
                }
                else if (collision.gameObject.GetComponent<Rotacion_enemigo_estatico>() != null)
                {
                    rotacion_Enemigo_Estatico = collision.gameObject.GetComponent<Rotacion_enemigo_estatico>();
                    rotacion_Enemigo_Estatico.enemyDead = true;
                }
                BoxCollider[] colliders = collision.gameObject.GetComponents<BoxCollider>();
                foreach(BoxCollider collider in colliders)
                {
                    collider.enabled = false;
                }
                raycastDetection = collision.gameObject.GetComponent<RaycastDetection>();
                raycastDetection.active = false;
                
                actualEnemy = collision.gameObject;
                StartCoroutine(Waitfor10Seconds(actualEnemy));
                puedesmatar = false;
                matar = false;
                animator.SetTrigger("Kill");
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
