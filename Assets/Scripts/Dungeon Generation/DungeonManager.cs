using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public GameObject startingRoom;
    public int roomMax;
    public int currentRooms = 0;
    public bool editBuffer = false;
    public bool complete = false;
   


    public List<RoomManager> activeRooms = new List<RoomManager>();
    public int nextRoom = 0;

    private void Awake()
    {
        startingRoom = GameObject.FindGameObjectWithTag("StartingRoom");
        startingRoom.GetComponent<RoomManager>().AssignConnectors();
    }

   

    public void NextIteration()
    {
        if (currentRooms < roomMax)
        {
            editBuffer = true;
            nextRoom++;
            if (activeRooms[nextRoom].finished == false) 
            {
                activeRooms[nextRoom].AssignConnectors();
            };
        }
        else
        {
            print("max reached");
            return;
        }
    }


    public void CloseWalls()
    {
        foreach (var room in activeRooms)
        {

            foreach (var connector in room.connectors)
            {
                if (connector.isDoor == false)
                {
                    connector.EnableWall();
                }
            }

            room.CheckOverlap();

        }
    }

}
