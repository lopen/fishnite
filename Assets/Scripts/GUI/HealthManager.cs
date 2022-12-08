using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public Sprite heart;
    private List<GameObject> hearts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        for (int i = 0; i < playerHealth.getHealth(); i++)
        {
            createHeart(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    GameObject createHeart(int i)
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

    void grayHeart()
    {
        for (int i = hearts.Count - 1; i >= 0; i--)
        {
            Image img = hearts[i].GetComponent<Image>();
            Color c = img.color;
            if (c.a == 1)
            {
                img.color = new Color(c.r, c.g, c.b, 0.3f);
                break;
            }
        }
    }

    void ungrayHeart()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            Image img = hearts[i].GetComponent<Image>();
            Color c = img.color;
            if (c.a != 1)
            {
                img.color = new Color(c.r, c.g, c.b, 1f);
                break;
            }
        }
    }

    void addHeart()
    {
        
    }

    void removeHeart()
    {
        hearts.RemoveAt(hearts.Count - 1);
    }
}
