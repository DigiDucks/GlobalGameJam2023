using System;
using System.Collections;
using System.Collections.Generic;
using GGJ2023;
using Unity.VisualScripting;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [SerializeField] private AudioClip countDown;

    private AudioSource _source;
    private RandomSoundVariant rsv;

    private AudioSource Source
    {
        get
        {
            if(!_source)
                _source = GetComponent<AudioSource>();

            return _source;
        }
    }

    private void Start()
    {
        rsv = GetComponent<RandomSoundVariant>();
        _source.loop = false;
    }

    private void OnEnable()
    {
        EventManager.ArenaChange.AddListener(PlayCountdown);
    }

    private void PlayCountdown()
    {
        Source.PlayOneShot(countDown);
    }

    public void PlayPickup()
    {
        rsv.RandomSoundVariantPlay();
    }
}