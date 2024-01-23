using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotación_Luz_Sirena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float velocidadRotacion = 360f;

    void Update()
    {
        // Calcula el ángulo de rotación por segundo
        float anguloRotacion = velocidadRotacion * Time.deltaTime;

        // Realiza la rotación sobre el eje Y del objeto
        transform.Rotate(0f, anguloRotacion, 0f);
    }
}
