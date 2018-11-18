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
    protected RuntimeAnimatorController myAnimatorController;
    protected Fighter myFighter;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimatorController = GetComponent<RuntimeAnimatorController>();
        myFighter = GetComponent<Fighter>();
        init();
    }

    protected abstract void init();

    public void changeToFighter(Vector2 anchor, float transitionTime)
    {
        myFighter.enabled = true;
        myFighter.Init(anchor, transitionTime);
        this.enabled = false;
        DisableCollider();
        //myRigidBody.isKinematic = true;
    }

    protected abstract void DisableCollider();
    protected abstract void EnableCollider();
}
