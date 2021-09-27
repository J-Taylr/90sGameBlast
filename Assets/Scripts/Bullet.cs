using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float bulletSpeed = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void MoveBullet(Vector2 vel, float mag)
    {
        print("got here");
        rb.AddForce(vel * mag);
        rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //damage enemy
        }

    }
}
