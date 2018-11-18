using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fighter : MonoBehaviour {

    public string creatureName;
    public int hp;
    public List<Attack> attacks;

    protected Rigidbody2D myRigidBody;
    public Rigidbody2D MyRigidBody
    {
        get
        {
            return myRigidBody;
        }
    }
    private RuntimeAnimatorController myAnimatorController;
    private Platformer myPlatformer;

    private float toZeroCounter; // set velocity to 0 when this hits 0
    private Vector2 anchorPoint;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimatorController = GetComponent<RuntimeAnimatorController>();
        myPlatformer = GetComponent<Platformer>();
    }

    public void useAttack()
    {
        // code
    }

    void FixedUpdate()
    {

        if (toZeroCounter > 0.0f)
        {
            toZeroCounter -= Time.deltaTime;
        }
        else
        {
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.position = anchorPoint;
            SubclassUpdate();
        }
    }

    protected abstract void SubclassUpdate();

    public void Init(Vector2 anchor, float transitionTime)
    {
        anchorPoint = anchor;
        toZeroCounter = transitionTime;
    }
}
