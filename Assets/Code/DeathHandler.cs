using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public GameObject screen;
    public float timeBeforeExit;
    private float timeBeforeExitLeft;
    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        GGJ2023.EventManager.PlayerDeath.AddListener(BlueScreenEnable);
        timeBeforeExitLeft = timeBeforeExit;
        activated = false;
        screen = transform.GetChild(0).gameObject;
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
        activated = true;
        screen.SetActive(true);
    }
}
