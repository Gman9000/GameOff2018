using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : Fighter
{
    protected override void SubclassUpdate()
    {
        // attack when off cooldown
        if (actionBarCounter >= MAX_ACTION_BAR)
        {
            UseAttack(SelectAttack());
        }
    }

    private int SelectAttack()
    {
        return Random.Range(0, attacks.Count - 1);
    }
}
