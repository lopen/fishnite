using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    private Player player; // Defines player
    public AudioClip collectSound; // Defines collect sound

    // Start is called before the first frame update / Defines player instance
    void Start()
    {
        player = Player.instance;
    }

    // On trigger enter / Used to detect player pickup
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            player.GetComponent<PlayerHealth>().increaseHealth();
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            Destroy(gameObject);
        }
    }
}
