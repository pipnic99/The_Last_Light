using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStart : MonoBehaviour
{
    private float randomNumber;
    private bool notStarted = true;
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
    // Update is called once per frame
    void Update()
    {
        if(notStarted)
        {
            Invoke("StartParticles", randomNumber);
            notStarted = false;
        }
    }
}
