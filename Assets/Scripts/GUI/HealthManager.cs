using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    
    public static HealthManager instance; // Define instance
    private PlayerHealth playerHealth; // Player health
    public Sprite heart; // Heart sprite
    private List<GameObject> hearts = new List<GameObject>(); // Current drawn hearts so they can be grayed out

    // Start is called before the first frame update / Used for checking instance & generating initial health
    void Start()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        // Generate starting hearts based on players start health
        playerHealth = Player.instance.GetComponent<PlayerHealth>();
        for (int i = 0; i < playerHealth.getHealth(); i++) {
            createHeart(i);
        }
    }

    // Create a heart container with the heart sprite in it, name of object is heart + i
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

    // Change heart opacity to 0.3f / Used when damage has been taken
    public void grayHeart()
    {
        for (int i = hearts.Count - 1; i >= 0; i--) {
            if (changeHeartOpacity(i, 0.3f)) {
                break;
            }
        }
    }

    // Change heart opacity to 1f / Used when health restored
    public void ungrayHeart()
    {
        for (int i = 0; i <= hearts.Count; i++) {
            if (changeHeartOpacity(i, 1f)) {
                break;
            }
        }
    }

    // If heart opacity is not equal to f then set it to f
    bool changeHeartOpacity(int i, float f)
    {
        Image img = hearts[i].GetComponent<Image>();
        Color c = img.color;
        if (c.a != f) {
            img.color = new Color(c.r, c.g, c.b, f);
            return true;
        }
        return false;
    }

    // Add a new heart to the GUI
    public void addHeart()
    {
        createHeart(hearts.Count);
        // Set opacity to 0.3f / used for hearts that are available but not currently filled
        changeHeartOpacity(hearts.Count - 1, 0.3f);
    }
    
    // Remove heart from list of hearts
    public void removeHeart()
    {
        hearts.RemoveAt(hearts.Count - 1);
    }
}
