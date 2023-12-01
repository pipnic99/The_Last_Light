using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_script : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private CapsuleCollider capsuleCollider;
    public AccionJugador accionBotonJugador;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(accionBotonJugador.laserActivo)
        {
            meshRenderer.enabled = true;
            capsuleCollider.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
            capsuleCollider.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Marcamos un condicionante para asegurarnos de que solo se active cuando el otro objeto tiene la etiqueta Player.
        if (other.CompareTag("Player"))
        {
            // Ponemos el valor de subirEscaleras a true.
            Debug.Log("Has tocado el laser");
        }
    }
}
