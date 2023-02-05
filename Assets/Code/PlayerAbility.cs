using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float shotCooldown; // Rate of Fire
    private float shotCooldownLeft;
    public int bulletsPerShot;
    public int damagePerBullet;
    public float bulletLifetime;
    public float bulletVelocity;

    // Special Effects
    // ----Burn Effect
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

    [Header("Debug Enemy Spawning")]
    public GameObject cubePrefab;
    public PlayerTargeting playerTargetingScript;

    void Start()
    {
        shotCooldownLeft = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject enemy = Instantiate(cubePrefab, playerTargetingScript.Aim(), Quaternion.identity);
            enemy.GetComponent<ChaseTarget>().target = gameObject.GetComponent<PlayerController>();
        }

        if(shotCooldownLeft > 0.0f)
            shotCooldownLeft -= Time.deltaTime;
        if(shotCooldownLeft <= 0.0f && Input.GetKey(KeyCode.Mouse0))
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Bullet bulletInstComp = bulletInstance.GetComponent<Bullet>();
            bulletInstComp.bulletVelocity = bulletVelocity;
            bulletInstComp.timeUntilDestroy = bulletLifetime;
            bulletInstComp.damage = damagePerBullet;
            bulletInstance.tag = "Player";

            // Burn
            bulletInstComp.burnDamagePerTick = burnDamagePerTick;
            bulletInstComp.burnDuration = burnDuration;
            bulletInstComp.burnTickDuration = burnTickDuration;

            // Slow
            bulletInstComp.slowSpeed = slowSpeed;
            bulletInstComp.slowDuration = slowDuration;

            // Chain
            bulletInstComp.numChains = numChains;
            bulletInstComp.damagePerChain = damagePerChain;
            bulletInstComp.chainRange = chainRange;

            // Bomb
            bulletInstComp.bombRange = bombRange;
            bulletInstComp.bombDamage = bombDamage;

            shotCooldownLeft = shotCooldown;
        }
    }
}
