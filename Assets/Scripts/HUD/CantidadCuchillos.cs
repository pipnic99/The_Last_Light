using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CantidadCuchillos : MonoBehaviour
{
    public int numeroCuchillos = 0;

    public Text textoVariable;
    // Update is called once per frame
    void Update()
    {
        textoVariable.text = "X " + numeroCuchillos.ToString();
    }
}
