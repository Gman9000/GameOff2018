using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platformer : MonoBehaviour {

    protected Rigidbody2D myRigidBody;
    public Rigidbody2D MyRigidBody
    {
        get
        {
            return myRigidBody;
        }
    }
    protected BoxCollider2D myBoxCollider;
    public BoxCollider2D MyBoxCollider
    {
        get
        {
            return myBoxCollider;
        }
    }
    protected Animator myAnimatorController;
    protected Fighter myFighter;
    public Fighter MyFighter
    {
        get
        {
            return myFighter;
        }
    }

    // point to return to after battle;
    private Vector2 returnPoint;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimatorController = GetComponent<Animator>();
        myFighter = GetComponent<Fighter>();
        SetReturnPoint();
        Init();
    }

    public void Init()
    {
        myRigidBody.position = returnPoint;
        SubclassInit();
        EnableCollider();
    }

    protected abstract void SubclassInit();

    public void changeToFighter(Vector2 anchor, float transitionTime)
    {
        myFighter.enabled = true;
        myFighter.Init(anchor, transitionTime);
        //this.GetComponent<PlayerPlatformer>().enabled = false;
        this.enabled = false;
        DisableCollider();
        //myRigidBody.isKinematic = true;
    }

    public void SetReturnPoint() { returnPoint = myRigidBody.position; }
    public void SetReturnPoint(Checkpoint c) { returnPoint = c.transform.position; }
    public void Return() { myRigidBody.position = returnPoint; }

    protected abstract void DisableCollider();
    protected abstract void EnableCollider();
}
