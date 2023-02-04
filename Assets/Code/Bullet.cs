using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float bulletVelocity;
    [HideInInspector]
    public float timeUntilDestroy;
    [HideInInspector]
    public int damage;

    // Update is called once per frame
    void Update()
    {
        // Movement update
        transform.position += (transform.forward * (bulletVelocity * Time.deltaTime));

        // Life cycle update
        timeUntilDestroy -= Time.deltaTime;
        if(timeUntilDestroy <= 0.0f)
            Destroy(gameObject);
    }
}
