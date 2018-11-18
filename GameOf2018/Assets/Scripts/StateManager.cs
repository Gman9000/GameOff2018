using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public static StateManager instance;

    public Camera fighterCamera;
    public Camera pBridgette;
    public Camera bBridgette;

    public GameObject BlackScreen;
    private float transitionTime = 0.25f;
    private float blackScreenScaleRate = 0.0f;

    private float distanceToFloor = 1.5f;
    public float DistanceToFloor
    {
        get
        {
            return distanceToFloor;
        }
    }

    private float distanceFromCamera = 2.0f;
    public float DistanceFromCamera
    {
        get
        {
            return distanceFromCamera;
        }
    }

    public Vector2 FighterOrigin
    {
        get
        {
            return fighterCamera.transform.position;
        }
    }

    void Awake()
    {
        if (instance != null) {
            Destroy(instance);
        }
        instance = this;
    }

    public void StartCombat(PlayerPlatformer bridgette, EnemyPlatformer enemy, Vector2 collisionPoint)
    {
        bridgette.MyRigidBody.position = (Vector2)fighterCamera.transform.position - (collisionPoint - bridgette.MyRigidBody.position);
        enemy.MyRigidBody.position = (Vector2)fighterCamera.transform.position - (collisionPoint - enemy.MyRigidBody.position);

        // jump away from each other to fixed positions
        Vector2 bAnchor = new Vector2(fighterCamera.transform.position.x - distanceFromCamera, fighterCamera.transform.position.y - distanceToFloor + (bridgette.MyBoxCollider.size.y / 2.0f - bridgette.MyBoxCollider.offset.y));
        Vector2 bDir =  bAnchor - bridgette.MyRigidBody.position;
        Vector2 eAnchor = new Vector2(fighterCamera.transform.position.x + distanceFromCamera, fighterCamera.transform.position.y - distanceToFloor + enemy.CenterToBottom);
        Vector2 eDir = eAnchor - enemy.MyRigidBody.position;

        bridgette.MyRigidBody.velocity = bDir / transitionTime;
        enemy.MyRigidBody.velocity = eDir / transitionTime;

        bridgette.changeToFighter(bAnchor, transitionTime);
        enemy.changeToFighter(eAnchor, transitionTime);

        SwitchToCamera(fighterCamera);
        blackScreenScaleRate = -400f;
    }

    private void SwitchToCamera(Camera c)
    {
        fighterCamera.enabled = false;
        pBridgette.enabled = false;
        bBridgette.enabled = false;

        c.enabled = true;
    }

    void FixedUpdate()
    {
        float newScale = BlackScreen.transform.localScale.x + blackScreenScaleRate * Time.deltaTime;
        BlackScreen.transform.localScale = new Vector2(newScale, newScale);

        if (BlackScreen.transform.localScale.x <= 0.0f)
        {
            BlackScreen.transform.localScale = new Vector2();
            blackScreenScaleRate = 0.0f;
        }
    }
}
