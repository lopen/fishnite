using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // player health
    public int health = 3;
    // max player health, can increase
    private int maxHealth = 3;
    // hard limit on how much maxHealth can increase
    private int hardHealthLimit = 5;

    // healthmanager instance, for GUI hearts
    private HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        healthManager = HealthManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // increase health if below maxHealth
    public void increaseHealth()
    {
        if (health < maxHealth)
        {
            health++;
            healthManager.ungrayHeart();
        }
    }

    // decrease health if its larger than 0
    public void decreaseHealth()
    {
        if (health > 0)
        {
            health--;
            healthManager.grayHeart();
        }
    }

    // increase maxHealth, but not more than hard limit, also add GUI heart
    public void increaseMaxHealth()
    {
        if (maxHealth < hardHealthLimit) {
            maxHealth++;
            healthManager.addHeart();
        }
    }

    // decrease maxHealth, not really used much
    public void decreaseMaxHealth()
    {
        if (maxHealth > 0)
        {
            maxHealth--;
            if (health > maxHealth)
            {
                health = maxHealth;
                healthManager.removeHeart();
            }
        }
    }

    // get health
    public int getHealth()
    {
        return health;
    }

    // get max health
    public int getMaxHealth()
    {
        return maxHealth;
    }
}
