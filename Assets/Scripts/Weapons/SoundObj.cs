using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObj : MonoBehaviour
{
    private void Start()
    {
        var audio = GetComponent<AudioSource>();
        audio.Play();
        Destroy(gameObject, audio.clip.length);
    }
}
