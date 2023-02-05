using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChaseTarget : MonoBehaviour
{
    //vars
    public Transform target;
    public float speed = 5f;
    public int damageValue = 50;
    public GameObject impactEffect;
    public float explosionRadius = 0f;
    public float minDistanceFromTarget = 0f;


    private void Start()
    {
        target = PlayerController.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // if no target destroy
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        var direction = target.position - transform.position;

        if (direction.magnitude < minDistanceFromTarget)
            transform.Translate(direction.normalized * (-speed * Time.deltaTime), Space.World);
        else
            transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);

        //rotate obj to the target
        transform.LookAt(target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bc = other.gameObject.GetComponent<Bullet>();
        if (other.CompareTag("Player") && bc == null)
            HitTarget();
    }

    //spawn effect then explode or damage then destroy self
    void HitTarget()
    {
        //Debug.Log("HIT TARGET TRIGGER");

        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        //if explosion radius
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage();
        }

        //destroy self
        Destroy(gameObject);
    }

    //explode checks each tagged collider
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Damage();
            }
        }
    }

    //damage = script's take damage, destroy self
    void Damage()
    {
        PlayerController.Instance.TakeDamage(damageValue);

        //Debug.Log("Enemy dealt damage = " + damageValue);
    }
}