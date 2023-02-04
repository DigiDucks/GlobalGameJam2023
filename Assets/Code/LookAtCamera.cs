using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Vector3 LookPosition = new Vector3(0, 180, 0);

    //theres probs a better way to do this?
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(LookPosition);
    }
}
