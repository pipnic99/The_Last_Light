using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_script : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Button_script scriptBoton;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(scriptBoton.botonPulsado)
        {
            meshRenderer.enabled = false;
        }
    }
}
