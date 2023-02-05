using System;
using System.Collections;
using System.Collections.Generic;
using GGJ2023;
using Unity.VisualScripting;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioClip countDown;

    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.loop = false;
    }

    private void OnEnable()
    {
        EventManager.ArenaChange.AddListener(PlayCountdown);
    }

    private void PlayCountdown()
    {
        _source.PlayOneShot(countDown);
    }
}
