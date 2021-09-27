using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;

    Vector2 moveDirection;
    
    [Header("Move")]
    public float moveSpeed = 20.0f;

    [Header("Roll")]
    public float rollDelay = 0.5f;
    public float rollTime = 0.1f;
    public bool rollAvailable;



    [Header("Extras")]
    public GameObject dashParticle;


    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInputs();
        
    }
    void FixedUpdate()
    {
        Move();

        
    }


    public void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rollAvailable = false;
            StartCoroutine(playerRoll());
        }

    }

    public void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private IEnumerator playerRoll()
    {
        print("roll");
        var vel = rb.velocity;
        
        moveSpeed *= 2;
       var particle = Instantiate(dashParticle,transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(rollTime);
        moveSpeed /= 2;

        

        yield return new WaitForSeconds(rollDelay);
        rollAvailable = true;
        Destroy(particle , 2);
    }
   



}
