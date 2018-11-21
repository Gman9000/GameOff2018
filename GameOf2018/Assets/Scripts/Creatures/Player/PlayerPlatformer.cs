using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformer : Platformer
{


    // scripts
    public LevelManager theLevelManager;

    //Moving the player
    public float moveSpeed;

    public float stunCooldown;
    private float stunCounter;
    //jump handling
    public float jumpSpeed;
    public LayerMask ground;

    public int maxJumps;
    private int remainingJumps;
    private bool pressingJump;

    //wall jumping
    //public int maxWallJumps;
    //private int remainingWallJumps;
    public float wallJumpSpeed;
    public float wallJumpAngle;

    // Air Dashing
    public int maxAirDashes;
    private int remainingAirDashes;
    private bool pressingAirDash;

    public float airDashSpeed;
    public float gravityScale;

    // Use this for initialization
    protected override void init()
    {
        remainingJumps = 0;
        pressingJump = false;
        stunCounter = 0;
        remainingAirDashes = maxAirDashes;
    }

    protected override void DisableCollider()
    {
        myBoxCollider.enabled = false;
        myRigidBody.gravityScale = 0;
    }
    protected override void EnableCollider()
    {
        myBoxCollider.enabled = true;
        myRigidBody.gravityScale = 4;
    }

    void FixedUpdate()
    {
        // if the player is grounded, reset their jumps
        if (IsGrounded())
        {
            remainingJumps = maxJumps;
            remainingAirDashes = maxAirDashes;
        }
        // if the player is not grounded, but has full jumps, subtract one to prevent free air jump
        else if (remainingJumps >= maxJumps)
        {
            remainingJumps = maxJumps - 1;
        }

        if (IsTouchingWall())
        {
            stunCounter = 0.0f;
        }

        //Moving the Player
        if (stunCounter <= 0)
        {
            myRigidBody.gravityScale = gravityScale;
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
            else if(IsGrounded())
            {
                myRigidBody.velocity = new Vector3(0f, myRigidBody.velocity.y, 0f);
            }

        }

        // wall jump
        if (Constants.PlayerInput.IsPressingSpace && IsTouchingWall() && !IsGrounded() && !pressingJump)
        {
            stunCounter = 0.0f;
            myRigidBody.gravityScale = gravityScale;
            float wallJumpx = wallJumpSpeed * Mathf.Cos(wallJumpAngle) * transform.localScale.x;
            float wallJumpy = wallJumpSpeed * Mathf.Sin(wallJumpAngle);
            MyRigidBody.velocity = new Vector2(wallJumpx, wallJumpy);
            pressingJump = true;
            stunCounter = stunCooldown;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // multi jump
        //if a player is pressing jump, has jumps left to press, and isn't holding jump from a previous input, and we are not touching the wall jump
        else if (Constants.PlayerInput.IsPressingSpace && remainingJumps > 0 && !pressingJump)
        {
            myRigidBody.gravityScale = gravityScale;
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpSpeed, 0f);
            --remainingJumps;
            pressingJump = true;
        }
        else if (!Constants.PlayerInput.IsPressingSpace)
        {
            pressingJump = false;
        }

        // air dash
        if (!IsGrounded() && Constants.PlayerInput.IsPressingAirDash && !pressingAirDash && remainingAirDashes > 0)
        {
            pressingAirDash = true;
            myRigidBody.gravityScale = 0f;
            //localscale will take care of dashing in different directions
            float airDashx = (airDashSpeed * transform.localScale.x);
            float airDashy = 0f;
            myRigidBody.velocity = new Vector2(airDashx,airDashy);
            --remainingAirDashes;
            stunCounter = stunCooldown;

        }
        else if (!Constants.PlayerInput.IsPressingAirDash)
        {
            pressingAirDash = false;
        }
        if (stunCounter >= 0)
        {
            stunCounter -= Time.deltaTime;
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

    public bool IsTouchingWall()
    {
        Vector2 offset = MyBoxCollider.offset;
        if (transform.localScale.x < 0) offset.x *= -1;
        Vector2 position = MyRigidBody.position + offset;


        //if you are facing the left, make checks to see if the player is touching the wall on the left
        if (transform.localScale.x < 0)
        {

            Vector2 direction = Vector2.left;
            float xTopPosition = (float)(position.x + ((MyBoxCollider.size.x / 2) * transform.localScale.x * 0.95));
            float yTopPosition = position.y;
            float xBottomPosition = (float)(position.x + ((MyBoxCollider.size.x / 2) * transform.localScale.x * 0.95));
            float yBottomPosition = position.y;

            float distance = -(MyBoxCollider.size.x / 2) * transform.localScale.x * 1.05f;
            Vector2 positionTop = new Vector2(xTopPosition, yTopPosition);
            Vector2 positionBottom = new Vector2(xBottomPosition, yBottomPosition);

            RaycastHit2D hitTop = Physics2D.Raycast(positionTop, direction, distance, ground);
            RaycastHit2D hitBottom = Physics2D.Raycast(positionBottom, direction, distance, ground);

            Debug.DrawRay(positionTop, distance * direction, Color.red);
            Debug.DrawRay(positionBottom, distance * direction, Color.red);

            if ((hitTop.collider != null) || (hitBottom.collider != null))
            {
                return true;
            }

            return false;
        }
        //if you are facing the right, make checks to see if the player is touching the wall on the right
        else
        {

            Vector2 direction = Vector2.right;
            float xTopPosition = (float)(position.x + ((MyBoxCollider.size.x / 2) * transform.localScale.x * 0.95));
            float yTopPosition = position.y;
            float xBottomPosition = (float)(position.x + ((MyBoxCollider.size.x / 2) * transform.localScale.x * 0.95));
            float yBottomPosition = position.y;

            float distance = (MyBoxCollider.size.x / 2) * transform.localScale.x * 1.05f;
            Vector2 positionTop = new Vector2(xTopPosition, yTopPosition);
            Vector2 positionBottom = new Vector2(xBottomPosition, yBottomPosition);

            RaycastHit2D hitTop = Physics2D.Raycast(positionTop, direction, distance, ground);
            RaycastHit2D hitBottom = Physics2D.Raycast(positionBottom, direction, distance, ground);

            Debug.DrawRay(positionTop, distance * direction, Color.blue);
            Debug.DrawRay(positionBottom, distance * direction, Color.blue);

            if ((hitTop.collider != null) || (hitBottom.collider != null))
            {
                return true;
            }

            return false;
        }
    }
}

