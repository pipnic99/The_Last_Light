using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStart : MonoBehaviour
{
    private float randomNumber;
    private bool notStarted = true;
    private bool startAudio = false;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.value;
    }
    void StartParticles()
    {
        ParticleSystem particleSystem = this.gameObject.GetComponent<ParticleSystem>();
        particleSystem.Play();

    }
    void StartAudio()
    {
        audioSource.Play();
        Invoke("StartAudio", 2);
    }
    // Update is called once per frame
    void Update()
    {
        if(notStarted)
        {
            Invoke("StartParticles", randomNumber);
            notStarted = false;
            startAudio = true;
        }
        if(startAudio)
        {
            Invoke("StartAudio", 2 + randomNumber);
            startAudio = false;
        }
    }
}
