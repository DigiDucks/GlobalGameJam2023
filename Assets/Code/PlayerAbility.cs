using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public GameObject tempPrefab;
    public PlayerTargeting playerTargetingScript;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject enemy = Instantiate(tempPrefab, playerTargetingScript.Aim(), Quaternion.identity);
        }
    }
}
