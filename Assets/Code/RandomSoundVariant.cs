using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSoundVariant : MonoBehaviour
{
    //damage sound
    [Space] [Header("Random Sound")] private AudioSource soundSource;
    public AudioClip[] soundClips;
    private AudioClip currentClip;
    private float volume;
    private float pitch;
    public float volumeMin = 1;
    public float volumeMax = 1;
    public float pitchMin = 1;
    public float pitchMax = 1;

    private int index;

    AudioSource SoundSource
    {
        get
        {
            if (!soundSource)
                soundSource = GetComponent<AudioSource>();
            return soundSource;
        }
    }

    public void RandomSoundVariantPlay()
    {
        if (soundClips.Length > 0)
        {
            //get random clip
            index = UnityEngine.Random.Range(0, soundClips.Length);
            currentClip = soundClips[index];

            //make random volume and pitch
            volume = UnityEngine.Random.Range(volumeMin, volumeMax);
            pitch = UnityEngine.Random.Range(pitchMin, pitchMax);

            //set randoms
            SoundSource.volume = volume;
            SoundSource.pitch = pitch;

            //play clip
            SoundSource.PlayOneShot(currentClip);
        }
    }
}