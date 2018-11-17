using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public static StateManager instance;

    public Camera fighterCamera;
    public Camera pBridgette;
    public Camera bBridgette;

    void Awake()
    {
        if (instance != null) {
            Destroy(instance);
        }
        instance = this;
    }

    public void switchToFighting(PlayerPlatformer bridgette, EnemyPlatformer enemy, Vector2 collisionPoint)
    {
        bridgette.MyRigidBody.position = (Vector2)fighterCamera.transform.position - (collisionPoint - bridgette.MyRigidBody.position);
        enemy.MyRigidBody.position = (Vector2)fighterCamera.transform.position - (collisionPoint - enemy.MyRigidBody.position);

        bridgette.MyRigidBody.velocity = Vector2.zero;
        enemy.MyRigidBody.velocity = Vector2.zero;

        bridgette.changeToFighter();
        enemy.changeToFighter();

        switchToCamera(fighterCamera);
    }

    private void switchToCamera(Camera c)
    {
        fighterCamera.enabled = false;
        pBridgette.enabled = false;
        bBridgette.enabled = false;

        c.enabled = true;
    }
}
