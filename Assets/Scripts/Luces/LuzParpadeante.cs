using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzParpadeante : MonoBehaviour
{
    // Importamos Todo lo que necesitamos
    Light luz;
    private int numeroAleatorio;
    public float intervaloGeneracion;
    // Creamos un array para almacenar todos los objetos con la etiquetaa luz parpadeante
    private GameObject[] florescente;
    private Renderer[] renderers;
    private Material[] materiales;
    private Material controlEmision;
    // Start is called before the first frame update
    void Start()
    {
        // Importamos el componente luz y llamamos a la funcion Generar Numeros aleatorios
        luz = GetComponent<Light>();
        InvokeRepeating("GenerarNumeroAleatorio", 0f, intervaloGeneracion);
        florescente = GameObject.FindGameObjectsWithTag("LuzParpadeante");

        //Inicializamos los arrays
        renderers = new Renderer[florescente.Length];
        materiales = new Material[florescente.Length];

        //Llenamos los arrays con los renderers y materiales

        for(int i = 0; i< florescente.Length; i++)
        {
            renderers[i] = florescente[i].GetComponent<Renderer>();
            materiales[i] = renderers[i].material;
        }
    }

    IEnumerator EncenderLuz()
    {
        luz.intensity = 0.2f;
        for (int i = 0; i < florescente.Length; i++)
        {
            // Cambiamos el color de el emision a blanco, de esa manera emitira luz
            materiales[i].SetColor("_EmissionColor", Color.white);
        }
        // Esperar 1 segundo
        yield return new WaitForSeconds(4f);
    }
    // Update is called once per frame
    void Update()
    {
        if (numeroAleatorio == 7)
        {
            StartCoroutine(EncenderLuz());
        }
        else
        {
            luz.intensity = 0;
            for (int i = 0; i < florescente.Length; i++)
            {
                // Por ejemplo, cambiar el color de emisión de los materiales
                materiales[i].SetColor("_EmissionColor", Color.black);
            }
            
        }
    }
    void GenerarNumeroAleatorio()
    {
        // Genera un número aleatorio entre 1 y 10 (ambos incluidos)
        numeroAleatorio = Mathf.RoundToInt(Random.Range(1f, 10f));
    }
}
