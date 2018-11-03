using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    public float speed;
    public Transform initialPosition;
    private Rigidbody2D myRigidBody;
    // Use this for initialization
    void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myRigidBody.velocity = new Vector2(speed, 0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "resetPoint")
        {
            gameObject.transform.position = initialPosition.position;
        }
    }
}
