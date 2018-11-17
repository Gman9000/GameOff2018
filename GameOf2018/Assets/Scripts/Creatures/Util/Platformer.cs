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

    public abstract void init();

    public void changeToFighter()
    {
        //myFighter.enabled = true;
        this.enabled = false;
        myRigidBody.isKinematic = true;
    }
}
