/*
 * File: Singleton.cs
 * Author: Li Baum (a.baum)
 * Date: 11/22/2022
 * © 2022 DigiPen(USA) Corporation
 */

using UnityEngine;

/// <summary>
/// A singleton that allows its public methods of the Instance to be accessible from anywhere in the scene
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //NOTE: Singletons should not be used for storing data but rather have function that affect the entire scope
    //      ScriptableObjects should be used for data storage due to their accessibility 


    public static T Instance { get; private set; }


    protected virtual void Awake()
    {
        //Ensure there is only ever one in the scene 
        if (Instance != null) Destroy(gameObject);

        //Set the public static reference
        Instance = this as T;
    }

    protected void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
