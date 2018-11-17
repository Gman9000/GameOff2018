using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public EnemyPlatformer myPlatformer;
    public HitboxType hitBoxType;
    public int weakpointDamageRecieved;
    public int strongpointDamageDealt;


    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerPlatformer bridgette = collision.collider.GetComponent<PlayerPlatformer>();

        if (bridgette != null && bridgette.enabled)
        {
            StateManager.instance.switchToFighting(bridgette, myPlatformer, collision.GetContact(0).point);
        }
    }
}
