using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3; // Player health
    private int maxHealth = 3; // Max player health, can increase
    private int hardHealthLimit = 5; // Hard limit on how much maxHealth can increase

   
    private HealthManager healthManager;  // Healthmanager instance, for GUI hearts

    // Start is called before the first frame update / Assigns health manager instance
    void Start()
    {
        healthManager = HealthManager.instance;
    }

    // Increase health if below maxHealth
    public void increaseHealth()
    {
        if (health < maxHealth)
        {
            health++;
            healthManager.ungrayHeart();
        }
    }

    // Decrease health if its larger than 0
    public void decreaseHealth()
    {
        if (health > 0)
        {
            health--;
            healthManager.grayHeart();
        }
    }

    // Increase maxHealth, but not more than hard limit, also add GUI heart
    public void increaseMaxHealth()
    {
        if (maxHealth < hardHealthLimit) {
            maxHealth++;
            healthManager.addHeart();
        }
    }

    // Decrease maxHealth, not really used much
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

    // Get health
    public int getHealth()
    {
        return health;
    }

    // Get max health
    public int getMaxHealth()
    {
        return maxHealth;
    }
}
