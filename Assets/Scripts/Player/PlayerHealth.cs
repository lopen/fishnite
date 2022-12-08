using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private int health = 3;
    private int maxHealth = 3;
    private bool alive = true;

    private HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        healthManager = GameObject.FindWithTag("HealthManager").GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
        {
            // end game
        }
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
        } else
        {
            alive = false;
        }
    }

    public void increaseMaxHealth()
    {
        maxHealth++;
        healthManager.addHeart();
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
        } else
        {
            alive = false;
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
