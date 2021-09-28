using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWeapon : MonoBehaviour
{


    private Camera mainCamera;
    public GameObject firePoint;
    public GameObject bulletPrefab;
    private Rigidbody2D rb;

    public Vector2 offset;
    public float angle;
    public float hitRadius = 0.2f;
    public ContactFilter2D contactFilter;

    
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        mainCamera = Camera.main;
    }


    
    void Update()
    {
        GetMousePos();
        Attack();
        Shoot();
    }


    public void GetMousePos()
    {
        Vector3 mouse = Input.mousePosition;

        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.position);


        offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);

        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 180f, (-angle + 90));




       

    }


    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject GO = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            Bullet bulletinstance = GO.GetComponent<Bullet>();
            bulletinstance.MoveBullet(rb.velocity, rb.velocity.magnitude);
        }
        
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("attacking");

           

            Collider2D[] hitResults = Physics2D.OverlapCircleAll(firePoint.transform.position, hitRadius);
            foreach (var hitCollider in hitResults)
            {
                if (hitCollider == null)
                {
                    print("null");
                }


                if (hitCollider.gameObject.CompareTag("Enemy"))
                {
                    print("enemy smacked");
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(firePoint.transform.position, hitRadius);
    }
}


