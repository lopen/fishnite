using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    private PlayerHealth player;
    public AudioClip collectSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }
    void OnTriggerEnter(Collider other)
    {
        player.increaseHealth();
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
