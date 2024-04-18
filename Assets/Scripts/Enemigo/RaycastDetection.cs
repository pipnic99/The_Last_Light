using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{
    public float raycastDistance = 10f;
    public float raycastHeight = 3f;
    public bool done = false;
    private Vector3 startPosition;
    public GameManager gameManager;
    public Animator animator;
    public GameObject muzleFlash;
    public AudioSource audioSource;
    private Rotacion_enemigo_estatico rotacion_Enemigo_Estatico;
    private MovimientoEnemigo movimientoEnemigo;
    public bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (GetComponent<MovimientoEnemigo>() != null)
        {
            movimientoEnemigo = GetComponent<MovimientoEnemigo>();
        }
        else if(GetComponent<Rotacion_enemigo_estatico>() != null)
        {
            rotacion_Enemigo_Estatico = GetComponent<Rotacion_enemigo_estatico>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            startPosition = this.transform.position;
            startPosition.y += raycastHeight;
            RaycastHit hit;
            if (Physics.Raycast(startPosition, transform.forward, out hit, raycastDistance, 1 << 8))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    gameManager.IsAlive = false;
                    animator.SetBool("Shoot", true);
                    if (animator.GetFloat("ShotTiming") != 0 && !done)
                    {
                        gameManager.blood = true;
                        audioSource.Play();
                        muzleFlash.SetActive(true);
                        done = true;
                    }
                }
            }
            Debug.DrawRay(startPosition, transform.forward * raycastDistance, Color.green);
        }
        
        
    }
}
