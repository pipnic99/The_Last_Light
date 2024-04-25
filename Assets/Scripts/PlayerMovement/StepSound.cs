using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour
{
    public List<AudioClip> WalkSounnds;
    public AudioSource audioSource;

    public int pos;

    public void playSoundStep()
    {
        pos = (int)Mathf.Floor(Random.Range(0, WalkSounnds.Count));
        audioSource.PlayOneShot(WalkSounnds[pos]);
    }
}
