using System.Collections;
using System.Collections.Generic;
using GGJ2023;
using UnityEngine;
using Random = System.Random;

public class DeathHandler : MonoBehaviour
{
    public GameObject screen;
    public float timeBeforeExit;
    private float timeBeforeExitLeft;
    private bool activated;
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        GGJ2023.EventManager.PlayerDeath.AddListener(BlueScreenEnable);
        timeBeforeExitLeft = timeBeforeExit;
        activated = false;
        screen = transform.GetChild(0).gameObject;
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activated)
        {
            timeBeforeExitLeft -= Time.deltaTime;
            if(timeBeforeExitLeft <= 0.0f)
            {
                #if UNITY_STANDALONE
                    Application.Quit();
                #endif
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }
        }
    }

    void BlueScreenEnable()
    {
        _source.Stop();
        activated = true;
        screen.SetActive(true);
        
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        EventManager.KillAll.Invoke();
       
        yield return new WaitForSeconds(1f);
        GetComponent<RandomSoundVariant>().RandomSoundVariantPlay();
    }
}
