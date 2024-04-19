using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    private float startVolume;
    // Start is called before the first frame update
    void Start()
    {
        startVolume = audioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0f)
        {
            audioSource.volume = startVolume / 2;
        }
        else
        {
            audioSource.volume = startVolume;
        }
    }
}
