using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPuerta : MonoBehaviour
{
    private bool moverpuerta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            moverpuerta = true;
        }
        while(moverpuerta = true && this.transform.position.x < -3.4f)
        {
            transform.Rotate(0f, 0.1f, 0f);
        }
    }
}
