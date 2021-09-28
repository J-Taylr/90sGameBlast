using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    Vector3 velocity;

    public float moveSpeed = 0.3f;
    public bool moving = false;

    public Transform nextPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && Vector3.Distance(transform.position, nextPos.position) > 0.1)
        {
            transform.position = Vector3.SmoothDamp(transform.position, nextPos.position, ref velocity, moveSpeed);
        }
        else
        {
            moving = false;
        }
    }


    public void MoveCamera(Transform destination)
    {
        nextPos = destination;
        moving = true;
        
    }
}
