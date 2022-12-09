using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    // define instance
    public static HealthManager instance;

    // player health
    private PlayerHealth playerHealth;

    // heart sprite
    public Sprite heart;
    // current drawn hearts so they can be grayed out
    private List<GameObject> hearts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else if (instance != this) 
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        // generate starting hearts based on players start health
        playerHealth = Player.instance.GetComponent<PlayerHealth>();
        for (int i = 0; i < playerHealth.getHealth(); i++)
        {
            createHeart(i);
        }
    }

    // Update is called once per frame
    void Update() { }

    // create a heart contaienr with the heart sprite in it.
    // name of object is heart + i 
    private GameObject createHeart(int i)
    {
        GameObject hearthContainer = new GameObject("heart" + i);

        RectTransform trans = hearthContainer.AddComponent<RectTransform>();
        trans.transform.SetParent(gameObject.transform);
        trans.localScale = new Vector3(1, 0.8f, 1);
        trans.localPosition = new Vector3(110 * i,0,0);

        Image image = hearthContainer.AddComponent<Image>();
        image.sprite = heart;
        hearthContainer.transform.SetParent(gameObject.transform);

        hearts.Add(hearthContainer);

        return hearthContainer;
    }

    // change heart opacity to 0.3f
    public void grayHeart()
    {
        for (int i = hearts.Count - 1; i >= 0; i--)
        {
            if (changeHeartOpacity(i, 0.3f))
            {
                break;
            }
        }
    }

    // change heart opacity to 1f 
    public void ungrayHeart()
    {
        for (int i = 0; i <= hearts.Count; i++)
        {
            if (changeHeartOpacity(i, 1f))
            {
                break;
            }
        }
    }

    // if heart opacity is not equal to f then set it to f
    bool changeHeartOpacity(int i, float f)
    {
        Image img = hearts[i].GetComponent<Image>();
        Color c = img.color;
        if (c.a != f)
        {
            img.color = new Color(c.r, c.g, c.b, f);
            return true;
        }
        return false;
    }

    // add a new heart to the GUI
    public void addHeart()
    {
        createHeart(hearts.Count);
        // set opacity to 0.3f
        changeHeartOpacity(hearts.Count - 1, 0.3f);
    }
    
    // remove heart from list of hearts
    public void removeHeart()
    {
        hearts.RemoveAt(hearts.Count - 1);
    }
}
