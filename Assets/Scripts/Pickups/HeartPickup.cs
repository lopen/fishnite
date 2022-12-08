using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private GameObject player;
    private HealthManager healthManager;
    public AudioClip collectSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        healthManager = GameObject.FindWithTag("HealthManager").GetComponent<HealthManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerHealth>().increaseMaxHealth();
        healthManager.addHeart();
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
