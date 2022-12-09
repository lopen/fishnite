using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPickup : MonoBehaviour
{
    private Player player;
    public AudioClip collectSound;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            player.GetComponent<BoatController>().addNitrus();
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
