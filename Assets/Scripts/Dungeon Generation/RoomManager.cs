using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Components")]

    public DungeonManager dungeonManager;
    private CameraMover cam;
    public GameObject camPos;

    [Header("In-Game Attributes")]
    public bool roomActive = true;


    [Header("Dungeon Creation")]
    public GameObject roomPrefab;
    public bool isStartingRoom;
    public bool finished = false;
    public float checkradius = 1f;

    [Header("Connectors")]

    public Connector[] connectors;
    public float topOffset;
    public float bottomOffset;
    public float leftOffset;
    public float rightOffset;




    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraMover>();
       // dungeonManager = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>();
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
                    var newRoom = GO.GetComponent<RoomManager>();
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

    public void AllignRoom(Connector connector, RoomManager newRoom)
    {

        
        Vector3 offset;
        switch (connector.connectorPos)
        {
            case Connector.direction.TOP:
                offset = new Vector3(0, topOffset, 0);
                break;
            case Connector.direction.BOTTOM:
                offset = new Vector3(0, bottomOffset, 0);
                break;
            case Connector.direction.LEFT:
                offset = new Vector3(leftOffset, 0, 0);
                break;
            case Connector.direction.RIGHT:
                offset = new Vector3(rightOffset, 0, 0);
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            roomActive = true;
            cam.MoveCamera(camPos.transform);
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            roomActive = false;
        }
    }
}

