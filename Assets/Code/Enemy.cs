using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        //health = 1;   
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.GetComponent<Bullet>() != null)
        {
            health--;
            Destroy(coll.gameObject);
        }
    }
}
