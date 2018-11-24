using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fighter : MonoBehaviour {

    public Sprite profile; 
    public string creatureName;
    public int maxHealth;
    private int currentHealth;
    public List<Attack> attacks;

    protected Rigidbody2D myRigidBody;
    public Rigidbody2D MyRigidBody
    {
        get
        {
            return myRigidBody;
        }
    }
    private RuntimeAnimatorController myAnimatorController;
    private Platformer myPlatformer;
    public Platformer MyPlatformer
    {
        get
        {
            return myPlatformer;
        }
    }

    private float toZeroCounter; // set velocity to 0 when this hits 0
    private Vector2 anchorPoint;

    protected int attackIndex;
    protected float attackCounter; // timer for animation syncing of attack
    private bool tied;
    private bool interrupted;

    protected float MAX_ACTION_BAR = 100f;
    protected float actionBarCounter;
    protected float cooldownMultiplier = 1.0f;
    protected float TIE_MULTIPLIER = 0.25f;
    protected float INTERRUPTED_MULTIPLIER = 0.5f;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimatorController = GetComponent<RuntimeAnimatorController>();
        myPlatformer = GetComponent<Platformer>();
        currentHealth = maxHealth;
    }

    public void UseAttack(int index)
    {
        attackIndex = index;
        attackCounter = attacks[index].windUp + attacks[index].hitTime;
        actionBarCounter = 0.0f;
    }

    void FixedUpdate()
    {

        if (toZeroCounter > 0.0f)
        {
            toZeroCounter -= Time.deltaTime;
        }
        else
        {
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.position = anchorPoint;
            SubclassUpdate();

            // if you are in hit time, dmg
            if (attackCounter > 0.0f && attackCounter <= attacks[attackIndex].hitTime)
            {
                if (FightManager.instance.CheckForTie(this))
                {
                    // clash with opponent
                    Debug.Log("clash");
                }
            }
            ManageCooldowns();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // TODO play hit animation
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            // tell fightmanager that you died
            FightManager.instance.EndCombat(this);
        }
        // interrupt current attack
        else if (attackCounter > 0.0f)
        {
            attackCounter = 0.0f;
            interrupted = true;
            cooldownMultiplier = INTERRUPTED_MULTIPLIER;
            Debug.Log("interrupted");
        }
    }

    public float HealthPercent
    {
        get
        {
            return (float)currentHealth / (float)maxHealth;
        }
    }
    public float ActionPercent
    {
        get
        {
            return (float)actionBarCounter / (float)MAX_ACTION_BAR;
        }
    }

    protected abstract void SubclassUpdate();

    public void Init(Vector2 anchor, float transitionTime)
    {
        attackIndex = 0;
        tied = false;
        interrupted = false;

        anchorPoint = anchor;
        toZeroCounter = transitionTime;

        actionBarCounter = MAX_ACTION_BAR;

        SubclassInit(anchor, transitionTime);
    }

    protected abstract void SubclassInit(Vector2 anchor, float transitionTime);

    private void ManageCooldowns()
    {
        if (attackCounter > 0.0f)
        {
            attackCounter -= Time.deltaTime;

            if (attackCounter <= 0.0f)
            {
                // attack
                if (!tied && !interrupted) FightManager.instance.DealDamage(this, attacks[attackIndex].damage);
                else
                {
                    tied = false;
                    interrupted = false;
                }
                attackCounter = 0.0f;
            }
        }
        if (attackCounter <= 0.0f && actionBarCounter < MAX_ACTION_BAR)
        {
            actionBarCounter += Time.deltaTime / (attacks[attackIndex].cooldown * cooldownMultiplier) * MAX_ACTION_BAR;
        }
        else if (attackCounter <= 0.0f)
        {
            actionBarCounter = MAX_ACTION_BAR;
            cooldownMultiplier = 1.0f;
        }
    }

    public float GetTimeToHitZone()
    {
        if (attackCounter <= 0.0f) return float.MaxValue;

        return Mathf.Max(attackCounter - attacks[attackIndex].hitTime, 0.0f);
    }

    public void CauseTie() {
        tied = true;
        cooldownMultiplier = TIE_MULTIPLIER;
    }

    public void ChangeToPlatformer()
    {
        myPlatformer.enabled = true;
        myPlatformer.Init();
        this.enabled = false;
        SubclassEnd();
    }

    protected abstract void SubclassEnd();
}
