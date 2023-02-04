using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    private GameObject face;

    // Start is called before the first frame update
    void Start()
    {
        face = transform.GetChild(0).gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
            Destroy(gameObject);

        // Rotate sprite to face camera
        face.transform.LookAt(new Vector3(transform.position.x, transform.position.y + 30.0f, -25.5f));
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
