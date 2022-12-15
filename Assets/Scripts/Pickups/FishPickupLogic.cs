using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPickupLogic : MonoBehaviour
{
    // fishing minigame prefab
    [SerializeField] private GameObject minigame;
    // if the player is in a trigger for fishing they can fish
    private bool canFish = false;

    // player instance
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        StartCoroutine(fishingTime());
    }

    //Update is called once per frame
    void Update() { }

    // allows player to press "f" and start the minigame
    IEnumerator fishingTime() 
    {
        while (true) {
            if (canFish) {  
                if (Input.GetKeyDown("f")) {
                    Instantiate(minigame, (player.transform.position - player.transform.forward * 3 + player.transform.up * 2), player.transform.rotation);
                    Destroy(gameObject);
                }
            }
            yield return new WaitForSeconds(0.1f * Time.deltaTime);
        }
    }

    // have fishingspot move up and down with waves
    void FixedUpdate() 
    {
        float waveHeight = WaveManager.instance.getWaveHeight(transform.position.x, transform.position.z);
        if (transform.position.y < waveHeight || transform.position.y > waveHeight) {
            transform.position = new Vector3(transform.position.x, waveHeight, transform.position.z);
        }
    }

    // if player is in trigger set canFish to true
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            canFish = true;
        }
    }

    // if player leaves trigger set canFish to false
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            canFish = false;
        }
    }
}