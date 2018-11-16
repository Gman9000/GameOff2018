using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    public RuntimeAnimatorController attackAnimation;
    public int damage;
    public float cooldown;

}
