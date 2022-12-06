using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    private bool canSell = false;
    private GameObject player;
    private Score score;
    public AudioClip sellSound;

    private List<ItemData> playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        score = GameObject.FindWithTag("Score").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSell) {  
            if (Input.GetKeyDown("space")) {
                sellFish();
            }
        }
    }

    void sellFish() 
    {
        // sell fish in inventory
        Debug.Log("SELLING ALL YOUR FISH O H NO");
        playerInventory = player.GetComponent<PlayerInv>().inventory;

        foreach (ItemData fish in playerInventory) {
            score.IncreaseScoreBy(fish.weight);
            Debug.Log("You sold: " + fish.itemDisplayName + " for: " + fish.weight);
            AudioSource.PlayClipAtPoint(sellSound, transform.position);
        }
        player.GetComponent<PlayerInv>().inventory.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            canSell = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            canSell = false;
        }
    }
}
