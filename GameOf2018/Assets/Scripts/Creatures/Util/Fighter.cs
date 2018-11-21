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
    protected float attackCounter;

    protected float MAX_ACTION_BAR = 100f;
    protected float actionBarCounter;

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
                if (FightManager.instance.IsTie(this, attacks[attackIndex].windUp, attacks[attackIndex].hitTime))
                {
                    // clash with opponent
                }
                else
                {
                    // change opponent to hit animation
                    FightManager.instance.DealDamage(this, attacks[attackIndex].damage);
                }
                attackCounter = 0.0f;
            }

            ManageCooldowns();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            // tell fightmanager that you died
            FightManager.instance.EndCombat(this);
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
        anchorPoint = anchor;
        toZeroCounter = transitionTime;

        actionBarCounter = MAX_ACTION_BAR;
    }

    private void ManageCooldowns()
    {
        if (attackCounter > 0.0f)
        {
            attackCounter -= Time.deltaTime;
        }
        if (actionBarCounter < MAX_ACTION_BAR)
        {
            actionBarCounter += Time.deltaTime / attacks[attackIndex].cooldown * MAX_ACTION_BAR;
        }
        else
        {
            actionBarCounter = MAX_ACTION_BAR;
        }
    }

    public void changeToPlatformer()
    {
        myPlatformer.enabled = true;
        myPlatformer.Init();
        this.enabled = false;
    }
}
