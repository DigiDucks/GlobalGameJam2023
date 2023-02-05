using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerRef;
    private Vector3 diff;

    [SerializeField] private float BoundsZ = 6;
    [SerializeField] private float BoundsX = 2;
    [SerializeField] private float SmoothSpeed = 1f;
    [SerializeField] private float OffsetX = 2f;
    [SerializeField] private float OffsetZ = 2f;


    // Start is called before the first frame update
    void Start()
    {
        diff = gameObject.transform.position - playerRef.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRef != null)
        {
            Vector3 pt = playerRef.transform.position + new Vector3(OffsetX,0,OffsetZ);
            var pos = transform.position;

            pt.y = pos.y;

            var delta = Vector3.Lerp(pos, pt, SmoothSpeed);

            //Smoothly go towards the player while not going outside the arena [Li]
            gameObject.transform.position = new Vector3(Mathf.Clamp(delta.x, -BoundsX, BoundsX), pos.y,
                Mathf.Clamp(delta.z, -BoundsZ, 0));
        }
    }
}