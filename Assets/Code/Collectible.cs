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
            if(pa.burnDamagePerTick > 0)
            {
                if(pa.burnDuration == 0.0f)
                    pa.burnDuration = 2.0f;
                if(pa.burnTickDuration == 0.0f)
                    pa.burnTickDuration = 1.75f;
            }
            else if(pa.burnDuration > 0.0f)
            {
                if(pa.burnDamagePerTick == 0)
                    pa.burnDamagePerTick = 1;
                if(pa.burnTickDuration == 0.0f)
                    pa.burnTickDuration = 1.75f;
            }
            else if(pa.burnTickDuration > 0.0f)
            {
                if(pa.burnDamagePerTick == 0)
                    pa.burnDamagePerTick = 1;
                if(pa.burnDuration == 0.0f)
                    pa.burnDuration = 2.0f;
            }

            pa.slowSpeed += SlowSpeed;
            pa.slowDuration += SlowDuration;
            if(pa.slowSpeed > 0.0f && pa.slowDuration == 0.0f)
                pa.slowDuration = 2.0f;
            else if(pa.slowDuration > 0.0f && pa.slowSpeed == 0.0f)
                pa.slowSpeed = 1.0f;

            pa.numChains += NumChains;
            pa.damagePerChain += DamagePerChain;
            pa.chainRange += ChainRange;
            if(pa.numChains > 0)
            {
                if(pa.damagePerChain == 0)
                    pa.damagePerChain = 1;
                if(pa.chainRange == 0.0f)
                    pa.chainRange = 2.0f;
            }
            else if(pa.damagePerChain > 0)
            {
                if(pa.numChains == 0)
                    pa.numChains = 2;
                if(pa.chainRange == 0.0f)
                    pa.chainRange = 2.0f;
            }
            else if(pa.chainRange > 0.0f)
            {
                if(pa.numChains == 0)
                    pa.numChains = 2;
                if(pa.damagePerChain == 0)
                    pa.damagePerChain = 1;
            }

            pa.bombRange += BombRange;
            pa.bombDamage += BombDamage;
            if(pa.bombRange > 0.0f && pa.bombDamage == 0)
                pa.bombDamage = 1;
            else if(pa.bombDamage > 0 && pa.bombRange == 0.0f)
                pa.bombRange = 1.0f;

            SFXManager.Instance.PlayPickup();
            
            Destroy(gameObject);
        }
    }
}
