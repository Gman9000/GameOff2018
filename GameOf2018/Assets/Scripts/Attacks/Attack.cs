using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic Attack", menuName = "Attacks/Basic Attack")]
public class Attack : ScriptableObject
{
    public RuntimeAnimatorController attackAnimation;
    public int damage;
    public float cooldown;

    public float windUp;
    public float hitTime;
}
