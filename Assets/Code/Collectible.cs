using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int MaxHealthAdded;
    public int ArmorAdded;
    public float SpeedAdded;
    public float ShotCooldownAdded;
    public int ShotBulletCountAdded;
    public int BulletDamageAdded;
    public float BulletLifetimeAdded;
    public float BulletVelocityAdded;

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

            Destroy(gameObject);
        }
    }
}
