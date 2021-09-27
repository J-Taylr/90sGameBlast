using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public GameObject wall;
    public GameObject connection;
    public bool isWall = false;
    public bool isDoor = false;
    public enum direction {TOP, BOTTOM, LEFT, RIGHT}
    public direction connectorPos;



    public void EnableWall()
    {
        isWall = true;
        wall.SetActive(true);
    }

    public void DisableWall()
    {
        isWall = false;
        wall.SetActive(false);
    }
}
