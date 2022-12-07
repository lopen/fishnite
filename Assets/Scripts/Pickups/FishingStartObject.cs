using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingStartObject : MonoBehaviour
{

    private GameObject player;
    public AudioClip collectSound;
    [SerializeField] private GameObject minigameSpawnPoint;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter(Collider other) {
        //player.GetComponent<PowerUpStore>().addPowerup(jet);
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        GameObject uiTEST = Instantiate(minigameSpawnPoint, (player.transform.position - player.transform.forward * 3 + player.transform.up * 2), player.transform.rotation) as GameObject;
        


        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
