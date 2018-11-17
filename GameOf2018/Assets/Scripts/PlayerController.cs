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

    //jump handling
    public float jumpSpeed;
    public LayerMask WhatIsGround;
    private BoxCollider2D myCollider;


    //
    //Jump checking
    public KeyCode jump;
    public Transform groundCheck;
    public float groundCheckradius;




    public bool isGrounded;
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
    //public AudioSource throwSnowballSound;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        //myAnimator = GetComponent<Animator>();
        //theLevelManager = FindObjectOfType<LevelManager>();
        doublejump = false;
        isTouchingJumpableWallOnLeft = false;
        isTouchingJumpableWallOnRight = false;
        wallJumpTracker = wallJumps;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //snowBallcounter -= Time.deltaTime;

        //JumpSetup
        isGrounded = IsGrounded();
        isTouchingJumpableWallOnLeft = IsTouchingLeftWall();
        isTouchingJumpableWallOnRight = IsTouchingRightWall();
        //Moving the Player
        if (Input.GetKey(right))
        {
            myRigidBody.velocity = new Vector3(moveSpeed, myRigidBody.velocity.y, 0f);
            //keep the scaling intact
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKey(left))
        {
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
            //flip the player while keeping the scaling intact
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
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
                else if (Input.GetKeyDown(jump) && (wallJumpTracker > 0))
                {
                    if (isTouchingJumpableWallOnLeft)
                    {
                        myRigidBody.AddForce(new Vector2(5.0f * jumpSpeed, 2.0f * jumpSpeed), ForceMode2D.Impulse);
                        //myRigidBody.velocity = new Vector3(4*jumpSpeed, jumpSpeed, 0f);
                        //doublejump = false;
                        wallJumpTracker -= 1;
                    }
                    else if (isTouchingJumpableWallOnRight)
                    {
                        myRigidBody.AddForce(new Vector2(-5.0f * jumpSpeed, -2.0f * jumpSpeed), ForceMode2D.Impulse);
                        //myRigidBody.velocity = new Vector3(4*jumpSpeed, jumpSpeed, 0f);
                        //doublejump = false;
                        wallJumpTracker -= 1;
                    }

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

    public bool IsGrounded()
    {
        Vector2 position= myRigidBody.position;
        Vector2 direction = Vector2.down;
        float xRightPosition = (float)(position.x + ((myCollider.size.x / 2) * transform.localScale.x * 0.95));
        float yRIghtPosition = (float)(position.y - ((myCollider.size.y / 2) * transform.localScale.y * 0.95));
        float xLeftPosition = (float)(position.x - ((myCollider.size.x / 2) * transform.localScale.x * 0.95));
        float yLeftPosition = (float)(position.y - ((myCollider.size.y / 2) * transform.localScale.y * 0.95));

        float distance = (myCollider.size.y/2) * transform.localScale.y * 1.05f;
        Vector2 positionRight = new Vector2 (xRightPosition, yRIghtPosition);
        Vector2 positionLeft = new Vector2(xLeftPosition, yLeftPosition);

        RaycastHit2D hitLeft = Physics2D.Raycast(positionLeft, direction, distance, WhatIsGround);
        RaycastHit2D hitRight = Physics2D.Raycast(positionRight, direction, distance, WhatIsGround);
        if ( (hitLeft.collider != null) || (hitRight.collider != null))
        {
            return true;
        }

        return false;
    }

    public bool IsTouchingLeftWall()
    {
        Vector2 position = myRigidBody.position;
        Vector2 direction = Vector2.left;
        float xTopPosition = (float)(position.x - ((myCollider.size.x / 2) * transform.localScale.x * 0.95));
        float yTopPosition = (float)(position.y + ((myCollider.size.y / 2) * transform.localScale.y * 0.95));
        float xBottomPosition = (float)(position.x - ((myCollider.size.x / 2) * transform.localScale.x * 0.95));
        float yBottomPosition = (float)(position.y - ((myCollider.size.y / 2) * transform.localScale.y * 0.95));

        float distance = (myCollider.size.x / 2) * transform.localScale.x * 1.05f;
        Vector2 positionTop = new Vector2(xTopPosition, yTopPosition);
        Vector2 positionBottom = new Vector2(xBottomPosition, yBottomPosition);
        RaycastHit2D hitTop = Physics2D.Raycast(positionTop, direction, distance, whatIsJumpableWall);
        RaycastHit2D hitBottom = Physics2D.Raycast(positionBottom, direction, distance, whatIsJumpableWall);
        if ((hitTop.collider != null) || (hitBottom.collider != null))
        {
            return true;
        }

        return false;

    }

    public bool IsTouchingRightWall()
    {
        Vector2 position = myRigidBody.position;
        Vector2 direction = Vector2.right;
        float xTopPosition = (float)(position.x + ((myCollider.size.x / 2) * transform.localScale.x * 0.95));
        float yTopPosition = (float)(position.y + ((myCollider.size.y / 2) * transform.localScale.y * 0.95));
        float xBottomPosition = (float)(position.x + ((myCollider.size.x / 2) * transform.localScale.x * 0.95));
        float yBottomPosition = (float)(position.y - ((myCollider.size.y / 2) * transform.localScale.y * 0.95));

        float distance = (myCollider.size.x / 2) * transform.localScale.x * 1.05f;
        Vector2 positionTop = new Vector2(xTopPosition, yTopPosition);
        Vector2 positionBottom = new Vector2(xBottomPosition, yBottomPosition);
        RaycastHit2D hitTop = Physics2D.Raycast(positionTop, direction, distance, whatIsJumpableWall);
        RaycastHit2D hitBottom = Physics2D.Raycast(positionBottom, direction, distance, whatIsJumpableWall);
        if ((hitTop.collider != null) || (hitBottom.collider != null))
        {
            return true;
        }

        return false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "killzone")
        {
            theLevelManager.GameOver();
        }
    }


}

