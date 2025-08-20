using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;

public class PlayerMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Global variables can be used in any method in this class

    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    Vector2 moveDirection;
    public float jumpForce = 500f;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"),0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce));

        }

    }
    void FixedUpdate()
    {
        // Movement
        rb.AddForce(moveDirection * moveSpeed);
        


    }
}