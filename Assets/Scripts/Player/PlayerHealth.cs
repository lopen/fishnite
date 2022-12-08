using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private int health = 3;
    private int maxHealth = 3;
    private bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }

    public void decreaseHealth()
    {
        if (health > 0)
        {
            health--;
        } else
        {
            alive = false;
        }
    }

    public void increaseMaxHealth()
    {
        maxHealth++;
    }

    public void decreaseMaxHealth()
    {
        if (maxHealth > 0)
        {
            maxHealth--;
            if (health > maxHealth)
            {
                health = maxHealth;
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
