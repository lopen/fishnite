using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePrompt;
    [SerializeField] private GameObject GUI;

    // Player instance
    private Player player;
    // Players inventory, ItemData is fish
    private List<ItemData> playerInventory;

    // Score instance
    private Score score;
    
    // To check if the player can sell fish near the vendor
    private bool canSell = false;
    // Prefabs used to visualize fish sold
    public GameObject smallfish;
    public GameObject morefish;
    // Sound to play when fish is sold
    public AudioClip sellSound;
    private bool fishSold = false;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        score = Score.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // if player is in trigger for vendor and space is pressed sell last fish in inv
        if (canSell) {  
            if (Input.GetKeyDown("space")) {
                sellFish();
            }
        }
    }

    // Sell last fish in inventory and removes it from the player while playing sound and checking score
    void sellFish() 
    {
        playerInventory = player.GetComponent<PlayerInv>().inventory;
        if (playerInventory.Count > 0) {
            ItemData fish = playerInventory[playerInventory.Count - 1];
            score.IncreaseScoreBy(fish.weight);
            AudioSource.PlayClipAtPoint(sellSound, transform.position);
            player.GetComponent<PlayerInv>().RemoveItem(fish);
            fishSold = true;
            checkScore();
        }
    }

    // Checks the current score and updates the barrel on the dock with more fish
    void checkScore()
    {
        if (score.GetScore() > 10)
        {
            // Set first fishbarrel prefab active
            smallfish.SetActive(true);
            if (score.GetScore() > 20)
            {
                // Set the second fishbarrel prefab active
                morefish.SetActive(true);
            }
        }
    }

    // If player enters vendor trigger, allow player to sell
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            canSell = true;
        }
    }

    // If player exits vendor trigger, remove player sell ability
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            canSell = false;
        }
        if(fishSold == true) {
            GameObject notif = Instantiate(dialoguePrompt, GUI.transform);
        }
        fishSold = false;
    }
}
