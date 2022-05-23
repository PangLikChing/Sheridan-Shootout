using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const int moveSpeed = 5;
    const int jumpForce = 200;
    [SerializeField] KeyCode forward, backward, left, right, jump;

    bool isJumped = false;
    Rigidbody playerRigidbody;
    Transform playerTransform;

    void Awake()
    {
        //initialize
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerTransform = transform;
    }

    void Update()
    {
        if (Input.GetKey(forward))
        {
            //move forward
            moveForward();
        }

        if (Input.GetKey(backward))
        {
            //move backward
            moveBackward();
        }

        if (Input.GetKey(left))
        {
            //move left
            moveLeft();
        }

        if (Input.GetKey(right))
        {
            //move right
            moveRight();
        }

        if (Input.GetKeyDown(jump))
        {
            //jump
            jumpUp();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //see if the player is on the ground
        if(collision.gameObject.tag == "Ground")
        {
            isJumped = false;
        }
    }

    //move methods
    void moveForward()
    {
        playerTransform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

    void moveBackward()
    {
        playerTransform.position += transform.forward * Time.deltaTime * -moveSpeed;
    }

    void moveLeft()
    {
        playerTransform.position += transform.right * Time.deltaTime * -moveSpeed;
    }

    void moveRight()
    {
        playerTransform.position += transform.right * Time.deltaTime * moveSpeed;
    }

    void jumpUp()
    {
        //if the player in on the ground
        if(isJumped == false)
        {
            //jump
            playerRigidbody.AddForce(0, jumpForce, 0);
            //tell the game that the player is in the air
            isJumped = true;
        }
    }
}
