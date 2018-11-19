using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected PlayerPlatformer player;
    
    private void Start()
    {
        player.GetComponent <PlayerPlatformer>();
    }

}
