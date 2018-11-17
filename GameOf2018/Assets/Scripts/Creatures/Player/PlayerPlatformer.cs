using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformer : Platformer
{


    // scripts
    public LevelManager theLevelManager;

    //Moving the player
    public float moveSpeed;

    //jump handling
    public float jumpSpeed;
    public LayerMask ground;

    public int maxJumps;
    private int remainingJumps;
    private bool pressingJump;


    // Use this for initialization
    public override void init()
    {
        remainingJumps = 0;
        pressingJump = false;
    }

    void FixedUpdate()
    {
        // if the player is grounded, reset their jumps
        if (IsGrounded())
        {
            remainingJumps = maxJumps;
        }
        // if the player is not grounded, but has full jumps, subtract one to prevent free air jump
        else if (remainingJumps >= maxJumps)
        {
            remainingJumps = maxJumps - 1;
        }

        //Moving the Player
        if (Constants.PlayerInput.IsPressingRight)
        {
            myRigidBody.velocity = new Vector3(moveSpeed, myRigidBody.velocity.y, 0f);

            //keep the scaling intact
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (Constants.PlayerInput.IsPressingLeft)
        {
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);

            //flip the player while keeping the scaling intact
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            myRigidBody.velocity = new Vector3(0f, myRigidBody.velocity.y, 0f);
        }

        //if a player is pressing jump, has jumps left to press, and isn't holding jump from a previous input, jump
        if (Constants.PlayerInput.IsPressingUp && remainingJumps > 0 && !pressingJump)
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpSpeed, 0f);
            --remainingJumps;
            pressingJump = true;
        }
        else if (!Constants.PlayerInput.IsPressingUp)
        {
            pressingJump = false;
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

    // checks the grounded state of the BoxCollider2D
    public bool IsGrounded()
    {
        Vector2 offset = myBoxCollider.offset;
        if (transform.localScale.x < 0) offset.x *= -1;

        Vector2 position = myRigidBody.position + offset;
        Vector2 direction = Vector2.down;
        float xRightPosition = (float)(position.x + ((myBoxCollider.size.x / 2) * transform.localScale.x * 0.95));
        float yRIghtPosition = position.y;
        float xLeftPosition = (float)(position.x - ((myBoxCollider.size.x / 2) * transform.localScale.x * 0.95));
        float yLeftPosition = position.y;

        float distance = (myBoxCollider.size.y / 2) * transform.localScale.y * 1.05f;
        Vector2 positionRight = new Vector2(xRightPosition, yRIghtPosition);
        Vector2 positionLeft = new Vector2(xLeftPosition, yLeftPosition);

        RaycastHit2D hitLeft = Physics2D.Raycast(positionLeft, direction, distance, ground);
        RaycastHit2D hitRight = Physics2D.Raycast(positionRight, direction, distance, ground);

        Debug.DrawRay(positionRight, distance * direction, Color.green);
        Debug.DrawRay(positionLeft, distance * direction, Color.green);

        if ((hitLeft.collider != null) || (hitRight.collider != null))
        {
            return true;
        }

        return false;
    }
}

