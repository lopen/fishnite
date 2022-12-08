using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    private bool canSell = false;
    private GameObject player;
    private Score score;
    public GameObject smallfish;
    public GameObject morefish;
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

        checkScore();
    }

    void checkScore()
    {
        if (score.getScore() > 10)
        {
            // set first fish active
            smallfish.SetActive(true);
            if (score.getScore() > 20)
            {
                // mMOAR
                morefish.SetActive(true);
            }
        }
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
