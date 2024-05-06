using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MakeBlackFinal : MonoBehaviour
{
    public GameObject blackPanel;
    public float velocidadDesvanecimiento = 1f;
    private bool start = false;
    private Image imagen;
    // Start is called before the first frame update
    void Start()
    {
        imagen = blackPanel.GetComponent<Image>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            start = true;
            blackPanel.gameObject.SetActive(true);
        }
    }
    IEnumerator Aparecer()
    {
        while (imagen.color.a < 1)
        {
            Color colorActual = imagen.color;
            if (colorActual.a < 1)
            {
                colorActual.a += Time.unscaledDeltaTime * velocidadDesvanecimiento;
                imagen.color = colorActual;
                AudioListener.volume -= 0.005f;
            }
            
            yield return null;
        }
        SceneManager.LoadScene("Final_Scene");
    }
    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            StartCoroutine(Aparecer());

            start = false;
        }
    }
}
