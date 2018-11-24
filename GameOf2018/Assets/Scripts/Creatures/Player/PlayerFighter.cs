using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFighter : Fighter
{
    public List<Text> uiAttacks;

    private int uiStartingIndex;

    protected override void SubclassInit(Vector2 anchor, float transitionTime)
    {
        uiStartingIndex = 0;
        EnableAttackUI();
    }

    protected override void SubclassUpdate()
    {
        // update ui to display attacks
        for (int i = 0; i < uiAttacks.Count; ++i)
        {
            if (uiStartingIndex + i < attacks.Count)
            {
                uiAttacks[i].text = attacks[uiStartingIndex + i].name;
            }
            if (actionBarCounter >= MAX_ACTION_BAR && uiAttacks[i].color == Color.yellow)
            {
                uiAttacks[i].color = Color.green;
            }
            else if (actionBarCounter < MAX_ACTION_BAR && uiAttacks[i].color == Color.green)
            {
                uiAttacks[i].color = Color.yellow;
            }
        }

        // poll for attacking
    }

    public void ClickOnAttack(int uiIndex)
    {
        if (uiIndex + uiStartingIndex < attacks.Count && actionBarCounter >= MAX_ACTION_BAR)
        {
            UseAttack(uiIndex + uiStartingIndex);
        }
    }

    public void HoverOnAttack(int uiIndex)
    {
        if (uiIndex + uiStartingIndex < attacks.Count)
        {
            for (int i = 0; i < uiAttacks.Count; ++i)
            {
                if (i == uiIndex)
                {
                    uiAttacks[i].color = actionBarCounter >= MAX_ACTION_BAR ? Color.green : Color.yellow;
                }
                else
                {
                    uiAttacks[i].color = Color.white;
                }
            }
        }
    }

    public void DisableAttackUI()
    {
        foreach (Text attack in uiAttacks)
        {
            attack.color = Color.white;
            attack.gameObject.SetActive(false);
        }
    }

    public void EnableAttackUI()
    {
        foreach (Text attack in uiAttacks)
        {
            attack.gameObject.SetActive(true);
        }
    }

    protected override void SubclassEnd()
    {
        DisableAttackUI();
    }
}
