using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    private AudioSource audio;

    private float musicVolume = 0.5f;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        audio.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
