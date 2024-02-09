using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CantidadCuchillos : MonoBehaviour
{
    public int numeroCuchillos = 0;
    private GameObject[] hijos;

    void Start()
    {
        // Obtén todos los GameObjects hijos de este GameObject, excluyendo este GameObject mismo
        Transform[] hijosTransform = transform.GetComponentsInChildren<Transform>(true);

        // Crea un array para almacenar solo los GameObjects hijos
        hijos = new GameObject[hijosTransform.Length - 1];

        // Itera sobre los GameObjects hijos y omite el GameObject actual
        int index = 0;
        foreach (Transform hijoTransform in hijosTransform)
        {
            if (hijoTransform.gameObject != gameObject)
            {
                hijos[index] = hijoTransform.gameObject;
                index++;
            }
        }

        // Ahora el array "hijos" contiene todos los GameObjects hijos, excluyendo este GameObject
        // Puedes hacer lo que quieras con este array
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numeroCuchillos && i < hijos.Length; i++)
        {
            hijos[i].SetActive(true);
        }
        for (int i = numeroCuchillos; i < hijos.Length; i++)
        {
            hijos[i].SetActive(false);
        }
    }
}
