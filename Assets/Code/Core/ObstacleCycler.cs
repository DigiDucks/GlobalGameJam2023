using System;
using System.Collections;
using System.Collections.Generic;
using GGJ2023;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleCycler : MonoBehaviour
{

    [SerializeField] private List<GameObject> arenaLayouts;

    private GameObject currentLayout;
    private void OnEnable()
    {
        EventManager.ArenaChange.AddListener(NewObstacles);
    }

    private void OnDisable()
    {
        EventManager.ArenaChange.RemoveListener(NewObstacles);
    }

    void NewObstacles()
    {
        var layout = currentLayout;
        if (!layout)
            currentLayout = arenaLayouts[Random.Range(0, arenaLayouts.Count)];
        else
        {
            layout.SetActive(false);
            if (arenaLayouts.Count > 1)
            {
                while (layout == currentLayout)
                {
                    layout = arenaLayouts[Random.Range(0, arenaLayouts.Count)];
                }

                currentLayout = layout;
            }
        }
        
        currentLayout.SetActive(true);
    }
}
