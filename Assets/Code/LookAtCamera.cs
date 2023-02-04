using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offsetVal = new Vector3(0,5,0);

    //theres probs a better way to do this?
    private void LateUpdate()
    {
        Vector3 tempPos = playerTransform.position + offsetVal;

        transform.SetPositionAndRotation(tempPos, Quaternion.identity);
    }
}
