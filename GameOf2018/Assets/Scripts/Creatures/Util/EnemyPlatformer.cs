using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyPlatformer : Platformer
{
    public List<BoxCollider2D> hitboxes;
    private float centerToBottom;
    public float CenterToBottom
    {
        get
        {
            return centerToBottom;
        }
    }

    protected override void SubclassInit()
    {
        centerToBottom = 0.0f;
        foreach (BoxCollider2D hitbox in hitboxes)
        {
            //BoxCollider2D box = hitbox.GetComponent<BoxCollider2D>();
            centerToBottom = Mathf.Max(centerToBottom, hitbox.size.y / 2.0f - hitbox.offset.y);
        }

        // SubclassInit();
    }

    protected override void DisableCollider()
    {
        foreach (BoxCollider2D hitbox in hitboxes)
        {
            //BoxCollider2D box = hitbox.GetComponent<BoxCollider2D>();
            hitbox.enabled = false;
        }
        myRigidBody.gravityScale = 0;
    }
    protected override void EnableCollider()
    {
        foreach (BoxCollider2D hitbox in hitboxes)
        {
            //BoxCollider2D box = hitbox.GetComponent<BoxCollider2D>();
            hitbox.enabled = true;
        }
        myRigidBody.gravityScale = 4;
    }
}
