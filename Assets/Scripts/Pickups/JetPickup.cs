using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPickup : MonoBehaviour
{
    private GameObject player;
    public AudioClip collectSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter(Collider other) {
        //player.GetComponent<PowerUpStore>().addPowerup(jet);
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
