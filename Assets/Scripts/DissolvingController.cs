using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DissolvingController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;
    public MovimientoJugador movimientoJugador;
    private Material[] skinnedMaterials;
    // Start is called before the first frame update
    void Start()
    {
        if (skinnedMesh != null)
        {
            skinnedMaterials = skinnedMesh.materials;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(movimientoJugador.deathbylaser)
        {
            StartCoroutine(DissolveCo());
        }
    }
    IEnumerator DissolveCo ()
    {
        if(skinnedMaterials.Length > 0)
        {
            float counter = 0;
            while(skinnedMaterials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for(int i = 0; i< skinnedMaterials.Length; i++)
                {
                    skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
