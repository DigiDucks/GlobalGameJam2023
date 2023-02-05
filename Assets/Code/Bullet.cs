using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Main Parameters")]
    [HideInInspector]
    public float bulletVelocity;
    [HideInInspector]
    public float timeUntilDestroy;
    [HideInInspector]
    public int damage;

    [Header("Burn Effect")]
    public int burnDamagePerTick;
    public float burnDuration;
    public float burnTickDuration;
    // ----Slow Effect
    [Header("Slow Effect")]
    public float slowSpeed;
    public float slowDuration;
    // ----Chain Effect
    [Header("Chain Effect")]
    public int numChains;
    public int damagePerChain;
    public float chainRange;
    // ----Bomb Effect
    [Header("Bomb Effect")]
    public float bombRange;
    public int bombDamage;

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
