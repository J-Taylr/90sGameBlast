using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{ 
    [Header("Components")]
    GameObject player;
    private CameraMover cam;
    public GameObject camPos;
    [Tooltip("Leave blank if only one camera position")] public GameObject camScrollPos;

    [Header("In-Game Attributes")]
    public bool roomActive = true;
    public bool CamVariation;



    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraMover>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            roomActive = true;
            if (CamVariation == false)
            {
                cam.ScrollOff();
                cam.MoveCamera(camPos.transform);
            }
            else
            {
                GetClosestCamPos();
                cam.ScrollCamera(camPos.transform, camScrollPos.transform);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            roomActive = false;
        }
    }


    public void GetClosestCamPos()
    {
        if (Vector3.Distance(player.transform.position, camPos.transform.position) < Vector3.Distance(player.transform.position, camScrollPos.transform.position)) //if player is closer to the original camera position
        {
            cam.MoveCamera(camPos.transform);
        }
        else //if the player is closer to the second position camera
        {
            cam.MoveCamera(camScrollPos.transform);
        }
    }

   

}
