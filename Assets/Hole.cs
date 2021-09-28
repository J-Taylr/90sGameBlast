using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerController>();
            if (player.isRolling == false)
            {
                //get position before roll 
                //take damage
                //respawn back at pre roll position
            }
        }
    }
}
