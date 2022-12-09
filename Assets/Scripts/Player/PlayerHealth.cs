using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health = 3;
    private int maxHealth = 3;
    private int hardHealthLimit = 5;

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

    public void increaseHealth()
    {
        if (health < maxHealth)
        {
            health++;
            healthManager.ungrayHeart();
        }
    }

    public void decreaseHealth()
    {
        if (health > 0)
        {
            health--;
            healthManager.grayHeart();
        }
    }

    public void increaseMaxHealth()
    {
        if (maxHealth < hardHealthLimit) {
            maxHealth++;
            healthManager.addHeart();
        }
    }

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

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }
}
