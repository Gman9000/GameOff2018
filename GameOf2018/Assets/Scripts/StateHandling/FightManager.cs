using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour {

    public static FightManager instance;

    public Image bridgetteImage;
    public Text bridgetteName;
    public Image bridgetteMaxHealthBar;
    public Image bridgetteHealthBar;
    public Image bridgetteMaxActionBar;
    public Image bridgetteActionBar;
    private Fighter bridgette;

    public Image enemyImage;
    public Text enemyName;
    public Image enemyMaxHealthBar;
    public Image enemyHealthBar;
    public Image enemyMaxActionBar;
    public Image enemyActionBar;
    private Fighter enemy;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    public void Init(Fighter p1, Fighter p2)
    {
        bridgette = p1;
        bridgetteImage.overrideSprite = p1.profile;
        bridgetteName.text = p1.creatureName;

        enemy = p2;
        enemyImage.overrideSprite = p2.profile;
        enemyName.text = p2.creatureName;
    }

    public bool IsTie(Fighter fighter, float windUp, float hitTime)
    {
        // todo
        return false;
    }

    public void DealDamage(Fighter fighter, int damage)
    {
        Fighter victim = fighter == bridgette ? enemy : bridgette;

        victim.TakeDamage(damage);
    }

    public void EndCombat(Fighter fighter)
    {
        if (fighter == bridgette)
        {
            bridgette.MyPlatformer.SetReturnPoint(Checkpoint.activeCheckpoint);
            enemy.changeToPlatformer();
        }
        else
        {
            enemy.gameObject.SetActive(false);
        }
        bridgette.changeToPlatformer();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (bridgette != null && enemy != null)
        {
            // update ui
            float newScale = bridgette.HealthPercent * bridgetteMaxHealthBar.transform.localScale.x;
            bridgetteHealthBar.transform.localScale = new Vector2(newScale, bridgetteMaxHealthBar.transform.localScale.y);
            newScale = bridgette.ActionPercent * bridgetteMaxActionBar.transform.localScale.x;
            bridgetteActionBar.transform.localScale = new Vector2(newScale, bridgetteMaxActionBar.transform.localScale.y);

            newScale = enemy.HealthPercent * enemyMaxHealthBar.transform.localScale.x;
            enemyHealthBar.transform.localScale = new Vector2(newScale, enemyMaxHealthBar.transform.localScale.y);
            newScale = enemy.ActionPercent * enemyMaxActionBar.transform.localScale.x;
            enemyActionBar.transform.localScale = new Vector2(newScale, enemyMaxActionBar.transform.localScale.y);
        }
    }
}
