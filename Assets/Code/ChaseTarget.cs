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


    // Update is called once per frame
    void Update()
    {
        // if no target destroy
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //find direction and distance, if we would hit this frame, trigger HitTarget
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        //rotate obj to the target
        transform.LookAt(target);
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
            Damage(target);
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
            if (collider.tag == "Player")
            {
                Damage(collider.transform);
            }
        }
    }

    //damage = script's take damage, destroy self
    void Damage(Transform playerObject)
    {
        PlayerController playerScript = playerObject.GetComponent<PlayerController>();

        //null enemy component check
        if (playerScript != null)
        {
            playerScript.TakeDamage(damageValue);
        }
        Debug.Log("Enemy dealt damage = " + damageValue);
    }
}
