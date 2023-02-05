using System;
using System.Collections;
using System.Collections.Generic;
using GGJ2023;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float outchPitch = 0.4f;
   [SerializeField] private GameObject face;
    private Camera mainCamera;

    // Burn Handling
    private int burnDamagePerTick;
    private float burnDurationLeft;
    private float burnTickDuration;
    private float burnTickDurationLeft;
    // Slow Handling
    private float oldSpeed;
    private float slowDurationLeft; // Will likely be pointless
    // Chain Handling
    private bool chainedThisHit = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        EventManager.KillAll.AddListener(KillCommand);
    }
    private void OnDisable()
    {
        WaveSpawner.EnemiesInWave.Remove(this);
        EventManager.KillAll.RemoveListener(KillCommand);
    }

    // Update is called once per frame
    void Update()
    {
        if(burnDurationLeft > 0.0f)
        {
            burnTickDurationLeft -= Time.deltaTime;
            if(burnTickDurationLeft <= 0.0f)
            {
                health -= burnDamagePerTick;
                burnTickDurationLeft = burnTickDuration;
            }

            burnDurationLeft -= Time.deltaTime;
        }
        if(slowDurationLeft > 0.0f)
        {
            slowDurationLeft -= Time.deltaTime;
            if(slowDurationLeft <= 0.0f)
            {
                gameObject.GetComponent<ChaseTarget>().speed = oldSpeed;
                slowDurationLeft = 0.0f;
            }
        }
        chainedThisHit = false;

        if (health <= 0)
        {
            SFXManager.Instance.PlayOuch(outchPitch);
            Destroy(gameObject);
        }

        // Rotate sprite to face camera
        face.transform.LookAt(new Vector3(transform.position.x, transform.position.y + 30.0f, -25.5f));
        //face.transform.forward = mainCamera.transform.forward;
       // face.transform.Rotate(0, 180, 0);
    }

    private void KillCommand()
    {
        Destroy(gameObject);
    }



    void OnTriggerEnter(Collider coll)
    {
        Bullet bc = coll.gameObject.GetComponent<Bullet>();

        if(bc != null && bc.CompareTag("Player"))
        {
            health -= bc.damage;

            if(bc.burnDuration > 0.0f)
            {
                burnDamagePerTick = bc.burnDamagePerTick;
                burnDurationLeft = bc.burnDuration;
                burnTickDuration = bc.burnTickDuration;
                burnTickDurationLeft = bc.burnTickDuration;
            }
            if(bc.slowDuration > 0.0f)
            {
                if(slowDurationLeft == 0.0f)
                {
                    // Don't want to lose original speed on multiple procs
                    oldSpeed = gameObject.GetComponent<ChaseTarget>().speed;
                }
                
                gameObject.GetComponent<ChaseTarget>().speed = bc.slowSpeed;
                slowDurationLeft = bc.slowDuration; 
            }
            if(bc.numChains > 0)
            {
                int chainsLeft = bc.numChains;
                Enemy currentSource = this;

                while(chainsLeft > 0)
                {
                    currentSource.health -= bc.damagePerChain;
                    chainedThisHit = true;

                    // Something with chains
                    Collider[] hcs = Physics.OverlapSphere(currentSource.transform.position,
                                                           bc.chainRange);
                    foreach(Collider newColl in hcs)
                    {
                        Enemy other = newColl.gameObject.GetComponent<Enemy>();
                        if(other != null && other.chainedThisHit == false)
                        {
                            currentSource = other;
                            break;
                        }
                    }

                    if(currentSource.chainedThisHit)
                        break;
                    chainsLeft--;
                }
            }
            if(bc.bombDamage > 0.0f)
            {
                // check in area for enemies and damage
                Collider[] hcs = Physics.OverlapSphere(transform.position,
                                                       bc.bombRange);
                foreach(Collider newColl in hcs)
                {
                    Enemy other = newColl.gameObject.GetComponent<Enemy>();
                    if(other != null)
                        other.health -= bc.bombDamage;
                }
            }

            Destroy(coll.gameObject);
        }
    }
}
