using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    public string switchLevel;
    EnemyFighter bossFighter;
    LevelManager levelManager;
    // Use this for initialization
    void Start()
    {
        bossFighter = GetComponent<EnemyFighter>();
        levelManager = FindObjectOfType<LevelManager>();

    }

    void FixedUpdate()
    {
        if (bossFighter.enabled)
        {
            if (bossFighter.HealthPercent <= 0.3f)
            { // if Boss health less than 30%
                levelManager.NextLevel(switchLevel);
            }
        }

    }
}
