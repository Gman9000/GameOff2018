using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public static Checkpoint activeCheckpoint = null;

    private SpriteRenderer sprite;
    public bool isStartingCheckpoint;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.black;
        if (isStartingCheckpoint)
        {
            activate();
        }
    }

    public void activate()
    {
        if (activeCheckpoint != null)
        {
            activeCheckpoint.deactivate();
        }
        sprite.color = Color.white;
        activeCheckpoint = this;
    }

    public void deactivate()
    {
        sprite.color = Color.black;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this != activeCheckpoint && other.GetComponent<PlayerPlatformer>() != null)
        {
            activate();
        }
    }
}
