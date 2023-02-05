using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum Effects
    {
        None,
        Burn,
        Slow,
        Bomb,
    }
    
    [Header("Basic Stats")]
    public int MaxHealthAdded;
    public int ArmorAdded;
    public float SpeedAdded;
    public float ShotCooldownAdded;
    public int ShotBulletCountAdded;
    public int BulletDamageAdded;
    public float BulletLifetimeAdded;
    public float BulletVelocityAdded;

    [Header("Burn Effect")]
    public int BurnDamagePerTick;
    public float BurnDuration;
    public float BurnTickDuration;

    [Header("Slow Effect")]
    public float SlowSpeed;
    public float SlowDuration;

    [Header("Chain Effect")]
    public int NumChains;
    public int DamagePerChain;
    public float ChainRange;

    [Header("Bomb Effect")]
    public float BombRange;
    public int BombDamage;

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController pc = coll.gameObject.GetComponent<PlayerController>();
            pc.maxHealth += MaxHealthAdded;
            pc.armor += ArmorAdded;
            pc.baseSpeed += SpeedAdded;

            PlayerAbility pa = coll.gameObject.GetComponent<PlayerAbility>();
            pa.shotCooldown += ShotCooldownAdded;
            pa.bulletsPerShot += ShotBulletCountAdded;
            pa.damagePerBullet += BulletDamageAdded;
            pa.bulletLifetime += BulletLifetimeAdded;
            pa.bulletVelocity += BulletVelocityAdded;

            pa.burnDamagePerTick += BurnDamagePerTick;
            pa.burnDuration += BurnDuration;
            pa.burnTickDuration += BurnTickDuration;

            pa.slowSpeed += SlowSpeed;
            pa.slowDuration += SlowDuration;

            pa.numChains += NumChains;
            pa.damagePerChain += DamagePerChain;
            pa.chainRange += ChainRange;

            pa.bombRange += BombRange;
            pa.bombDamage += BombDamage;

            Destroy(gameObject);
        }
    }
}
