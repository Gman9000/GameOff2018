using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public EnemyPlatformer myPlatformer;
    public HitboxType hitBoxType;


    void OnCollisionEnter2D(Collision2D collision)
    {
        // tell level manager to switch to combat
    }
}
