using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [Header("Room Changing")]
    public float moveSpeed = 0.3f;
    public bool changeRooms = false;
    public Transform nextPos;

    [Header("Room Scrolling")]
    public bool scrollRoom = false;
    public float scrollSpeed = 0.1f;
    Vector3 targetPos;
    GameObject player;

    Transform firstScrollPos;
    Transform secondScrollPos;

    Vector3 velocity; //reference
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (changeRooms && Vector3.Distance(transform.position, nextPos.position) > 0.1)
        {
            transform.position = Vector3.SmoothDamp(transform.position, nextPos.position, ref velocity, moveSpeed);
        }
        else
        {
            changeRooms = false;
        }

      
    }


    private void LateUpdate()
    {
        if (scrollRoom && changeRooms == false)
        {
            targetPos = new Vector3(Mathf.Clamp(player.transform.position.x, firstScrollPos.position.x, secondScrollPos.position.x), Mathf.Clamp(player.transform.position.y, firstScrollPos.position.y, secondScrollPos.position.y), -10);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, scrollSpeed);
        }
    }


    public void MoveCamera(Transform destination)
    {
        nextPos = destination;
        changeRooms = true;
        
    }

    public void ScrollCamera(Transform firstPos, Transform secondPos)
    {
        firstScrollPos = firstPos;
        secondScrollPos = secondPos;

         scrollRoom = true;

        
    }

    public void ScrollOff()
    {
        scrollRoom = false;
    }
}
