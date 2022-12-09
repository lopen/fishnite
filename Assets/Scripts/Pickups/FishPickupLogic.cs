using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPickupLogic : MonoBehaviour
{

    [SerializeField] private GameObject minigame;
    private bool canFish = false;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        StartCoroutine(fishingTime());
    }

    //Update is called once per frame
    void Update() { }

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

    void FixedUpdate() 
    {
        float waveHeight = WaveManager.instance.getWaveHeight(transform.position.x, transform.position.z);
        if (transform.position.y < waveHeight || transform.position.y > waveHeight) {
            transform.position = new Vector3(transform.position.x, waveHeight, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            canFish = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            canFish = false;
        }
    }
}