using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f;
    public float acceleration = 10f;
    Vector3 movement;
    float movementSpeed;
    float timeParam = 0f;
    
    Vector3 startVector;
    Vector3 playerFacing;
    Vector3 playerFacingRight;
    Vector3 moveDirection;

    float moveX;
    float moveY;
    float moveAngle; //angle of movement relative to characters look angle
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;
    Vector3 cross;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move (float h, float v)
    {
        movement.Set(h, 0f, v);
        if(h != 0 || v != 0)
        {
            if(timeParam < 1)
            {
                timeParam += acceleration * Time.deltaTime;
            }
            else
            {
                timeParam = 1f;
            }
        }
        else
        {
            if(timeParam > 0)
            {
                timeParam -= acceleration * Time.deltaTime;
            }
            else
            {
                timeParam = 0f;
            }
        }

        
        movement = movement.normalized * speed * timeParam * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);  
        //transform.position = Vector3.Lerp(transform.position, transform.position + movement, acceleration * Time.deltaTime);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        Vector3 playerToMouse;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

   void Animating(float h, float v)
    {
        movementSpeed = movement.magnitude*10;
        moveDirection = movement.normalized;
        playerFacing = transform.forward;
        playerFacingRight = transform.right;

        moveAngle = Vector3.Angle(movement, playerFacing);
        cross = Vector3.Cross(playerFacing, movement);

        moveX = Vector3.Dot(playerFacingRight, moveDirection);
        moveY = Vector3.Dot(playerFacing, moveDirection);

        if(cross.y < 0)
        {
            moveAngle = -1*moveAngle;
        }

        anim.SetFloat("MoveX", moveX);
        anim.SetFloat("MoveY", moveY);
        anim.SetFloat("Speed", movementSpeed);
        anim.SetFloat("MoveAngle", moveAngle);

        //Debug.Log("moveX = " + moveX + " " + "moveY = " + moveY);
    }
}
