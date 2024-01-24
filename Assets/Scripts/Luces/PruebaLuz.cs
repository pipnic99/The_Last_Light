using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaLuz : MonoBehaviour
{
    Light luz;
    private int numeroAleatorio;
    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<Light>();
        InvokeRepeating("GenerarNumeroAleatorio", 0f, 0.1f);
    }

    IEnumerator EncenderLuz()
    {
        luz.intensity = 0.2f;
        // Esperar 1 segundo
        yield return new WaitForSeconds(4f);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(numeroAleatorio);
        if (numeroAleatorio == 7)
        {
            StartCoroutine(EncenderLuz());
        }
        else
        {
            luz.intensity = 0;
        }
    }
    void GenerarNumeroAleatorio()
    {
        // Genera un número aleatorio entre 1 y 10 (ambos incluidos)
        numeroAleatorio = Mathf.RoundToInt(Random.Range(1f, 10f));

        Debug.Log("Número aleatorio generado: " + numeroAleatorio);
    }

}
