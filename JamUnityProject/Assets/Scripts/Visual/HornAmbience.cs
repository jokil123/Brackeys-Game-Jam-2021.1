using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornAmbience : MonoBehaviour
{
    public AudioSource hornSound;

    void Start()
    {
        StartCoroutine(PlayHornSound());
    }

    IEnumerator PlayHornSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            hornSound.Play();
        }
    }
}
