using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundVariant : MonoBehaviour
{
    //damage sound
    [Space]
    [Header("Random Sound")]
    private AudioSource soundSource;
    public AudioClip[] soundClip;
    private AudioClip currentClip;
    private float volume;
    private float pitch;
    public float volumeMin;
    public float volumeMax;
    public float pitchMin;
    public float pitchMax;

    private int index;


    public void RandomSoundVariantPlay()
    {
        if(soundClip.Length > 0)
        {
            //get random clip
            index = UnityEngine.Random.Range(0, soundClip.Length);
            currentClip = soundClip[index];

            //make random volume and pitch
            volume = UnityEngine.Random.Range(volumeMin, volumeMax);
            pitch = UnityEngine.Random.Range(pitchMin, pitchMax);

            //set randoms
            soundSource.volume = volume;
            soundSource.pitch = pitch;

            //play clip
            soundSource.PlayOneShot(currentClip);
        }
    }
}
