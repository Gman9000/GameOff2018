using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private LevelManager theLevelManager;

    //Moving the player
    private Rigidbody2D myRigidBody;
    public float moveSpeed;

    public KeyCode left;
    public KeyCode right;

    //
    //Jump checking
    public KeyCode jump;
    public Transform groundCheck;
    public float groundCheckradius;
    public LayerMask WhatIsGround;
    public bool isGrounded;
    public float jumpSpeed;
    private bool doublejump;
    public int wallJumps;
    private int wallJumpTracker;
    
    //wallJump
    private bool isTouchingJumpableWallOnLeft;
    private bool isTouchingJumpableWallOnRight;
    public Transform wallJumpCheckLeft;
    public Transform wallJumpCheckRight;
    public float wallCheckRadius;
    public LayerMask whatIsJumpableWall;

    //Animation
    //private Animator myAnimator;

    // sound effects
    public AudioSource throwSnowballSound;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        //myAnimator = GetComponent<Animator>();
        //theLevelManager = FindObjectOfType<LevelManager>();
        doublejump = false;
        isTouchingJumpableWallOnLeft = false;
        isTouchingJumpableWallOnRight = false;
        wallJumpTracker = wallJumps;

    }


    // Update is called once per frame
    void Update()
    {
        //snowBallcounter -= Time.deltaTime;

        //JumpSetup
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckradius, WhatIsGround);
        isTouchingJumpableWallOnLeft = Physics2D.OverlapCircle(wallJumpCheckLeft.position, wallCheckRadius, whatIsJumpableWall);
        isTouchingJumpableWallOnRight = Physics2D.OverlapCircle(wallJumpCheckRight.position, wallCheckRadius, whatIsJumpableWall);
        //Moving the Player
        if (Input.GetKey(right))
        {
            myRigidBody.velocity = new Vector3(moveSpeed, myRigidBody.velocity.y, 0f);
            //keep the scaling intact
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKey(left))
        {
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
            //flip the player while keeping the scaling intact
            //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            myRigidBody.velocity = new Vector3(0f, myRigidBody.velocity.y, 0f);
        }

        //JumpCheck

        
        if (Input.GetKeyDown(jump))
        {
            if (isGrounded)
            {
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpSpeed, 0f);
                doublejump = true;
                wallJumpTracker = wallJumps;
            }
            else
            {

                if (doublejump)
                {
                    myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpSpeed, 0f);
                    doublejump = false;
                }
                else if (Input.GetKeyDown(jump) && (wallJumpTracker > 0) && (isTouchingJumpableWallOnLeft || isTouchingJumpableWallOnRight))
                {
                    myRigidBody.AddForce(new Vector2(myRigidBody.velocity.x, 2* jumpSpeed), ForceMode2D.Impulse);
                        //myRigidBody.velocity = new Vector3(4*jumpSpeed, jumpSpeed, 0f);
                    doublejump = false;
                    wallJumpTracker -= 1;

                }
            }

        }

        //myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {


        if (Input.GetKeyDown(jump) && (isTouchingJumpableWallOnLeft || isTouchingJumpableWallOnRight))
        {
                myRigidBody.velocity = new Vector3(-myRigidBody.velocity.x, -jumpSpeed/2, 0f);
          

        }
    }*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "killzone")
        {
            theLevelManager.GameOver();
        }
    }


}

