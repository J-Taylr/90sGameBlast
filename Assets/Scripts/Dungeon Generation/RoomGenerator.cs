using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [Header("Components")]
    
    public DungeonManager dungeonManager;
    
    [Header("Dungeon Creation")]
    public GameObject roomPrefab;
    public bool isStartingRoom;
    public bool finished = false;
    public float checkradius = 1f;

    [Header("Connectors")]

    public Connector[] connectors;
    public Vector3 topOffset;
    public Vector3 bottomOffset;
    public Vector3 leftOffset;
    public Vector3 rightOffset;




    private void Awake()
    {
        
       
        dungeonManager = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>();
    }


    public void BeginRoomCheck()
    {
        foreach (var connector in connectors)
        {
            CheckForWalls(connector);
        }
        AssignConnectors();
    }

    public void AssignConnectors()
    {

        foreach (var connector in connectors)
        {
            if (!connector.isWall || !connector.isDoor)
            {
                int rdm = Random.Range(0, 100);

                if (rdm <= 75 && dungeonManager.currentRooms < dungeonManager.roomMax)
                {
                    var GO = Instantiate(roomPrefab, connector.connection.transform.position, Quaternion.identity);
                    var newRoom = GO.GetComponent<RoomGenerator>();
                    AllignRoom(connector, newRoom);
                    dungeonManager.currentRooms++;
                    dungeonManager.activeRooms.Add(newRoom);
                    connector.isDoor = true;
                }

                else if (!connector.isDoor)
                {

                    connector.EnableWall();
                }

            }

        }
        finished = true;

        CheckDungeon();
    }

    public void CheckOverlap()
    {
        Collider2D[] overlapResult = Physics2D.OverlapCircleAll(transform.position, .1f);

        foreach (var collision in overlapResult)
        {
            if (collision.gameObject.CompareTag("Room") || collision.gameObject.CompareTag("StartingRoom"))
            {

                if (collision.gameObject != this.gameObject)
                {
                    print("overlap");
                    //dungeonManager.activeRooms.Remove(this);
                    Destroy(this.gameObject);
                }


            }
        }
    }


    public void CheckForWalls(Connector connector)
    {
        Collider2D[] hitResults = Physics2D.OverlapCircleAll(connector.transform.position, checkradius);

        foreach (var item in hitResults)
        {
            if (item.gameObject.CompareTag("Connector"))
            {
                connector.EnableWall();

            }
        }

    }

    public void AllignRoom(Connector connector, RoomGenerator newRoom)
    {


        Vector3 offset;
        switch (connector.connectorPos)
        {
            case Connector.direction.TOP:
                offset = topOffset;
                break;
            case Connector.direction.BOTTOM:
                offset = bottomOffset;
                break;
            case Connector.direction.LEFT:
                offset = leftOffset;
                break;
            case Connector.direction.RIGHT:
                offset = rightOffset;
                break;
            default:
                offset = new Vector3(0, 0, 0);
                break;
        }

        newRoom.transform.position += offset;

        foreach (var newconnectors in newRoom.connectors)
        {
            if (Vector2.Distance(connector.transform.position, newconnectors.transform.position) < 4)
            {
                newconnectors.isDoor = true;
                newconnectors.DisableWall();
            }

        }
    }


    public void CheckDungeon()
    {


        if (dungeonManager.currentRooms == dungeonManager.roomMax)
        {
            print("dungeon complete");
            dungeonManager.complete = true;
            dungeonManager.CloseWalls();
        }
        else
        {
            dungeonManager.NextIteration();
        }

    }


   
}

