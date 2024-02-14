using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FadeScript : MonoBehaviour
{
    [SerializeField] float fadeSpeed, fadeAumont;
    float originalOpacity;
    private string nombreDelMaterial = "Textura_Pared_Inv";

    Material Mat;
    public bool DoFade = false;
    // Start is called before the first frame update
    void Start()
    {
        // Buscar el material por nombre
        string[] guids = AssetDatabase.FindAssets("t:Material " + nombreDelMaterial);

        if (guids.Length > 0)
        {
            // Obtener la ruta del material
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);

            // Cargar el material
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);

            // Hacer algo con el material, como aplicarlo a un objeto
            Mat = GetComponent<Renderer>().material = material;
        }
        originalOpacity = Mat.color.a;

    }

    // Update is called once per frame
    void Update()
    {
        if (DoFade)

            FadeNow();

        else

            ResetFade();

    }

    void FadeNow()
    {
        Color currentColor = Mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAumont, fadeSpeed * Time.deltaTime));
        Mat.color = smoothColor;
    }

    void ResetFade()
    {
        Color currentColor = Mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
        Mat.color = smoothColor;

    }
}
