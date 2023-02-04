using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shotCooldown; // Rate of Fire
    private float shotCooldownLeft;
    public int bulletsPerShot;
    public int damagePerBullet;
    public float bulletLifetime;
    public float bulletVelocity;

    // Start is called before the first frame update
    void Start()
    {
        shotCooldownLeft = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(shotCooldownLeft > 0.0f)
            shotCooldownLeft -= Time.deltaTime;
        if(shotCooldownLeft <= 0.0f)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Bullet bulletInstComp = bulletInstance.GetComponent<Bullet>();
            bulletInstComp.bulletVelocity = bulletVelocity;
            bulletInstComp.timeUntilDestroy = bulletLifetime;
            bulletInstComp.damage = damagePerBullet;
            bulletInstance.tag = "Enemy";

            shotCooldownLeft = shotCooldown;
        }
    }
}
