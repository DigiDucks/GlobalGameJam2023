using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator CameraShaker(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        //Debug.Log("starting");

        float elapsed = 0.0f;

        //while timer runs
        while (elapsed < duration)
        {
            //random xz
            float x = Random.Range(-1, 1) * magnitude;
            float z = Random.Range(-1, 1) * magnitude;
            //Debug.Log("before move");

            transform.localPosition = new Vector3(x, originalPos.y, z );

            //Debug.Log("random is : " + x);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}