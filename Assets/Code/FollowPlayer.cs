using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerRef;
    private Vector3 diff;

    // Start is called before the first frame update
    void Start()
    {
        diff = gameObject.transform.position - playerRef.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRef != null)
        {
            Vector3 pt = playerRef.transform.position;

            if(pt.x < -19)
                pt.x = -19;
            else if(pt.x > 19)
                pt.x = 19;

            if(pt.z < -13)
                pt.z = -13;
            else if(pt.z > 13)
                pt.z = 13;

            gameObject.transform.position = pt + diff;
        }
    }
}
