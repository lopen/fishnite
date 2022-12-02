using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUp : MonoBehaviour
{
    private GameObject player;
    private GameObject timer;
    public AudioClip collectSound;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        timer = GameObject.FindWithTag("Timer");
    }

    void OnTriggerEnter(Collider other) {
        //player.GetComponent<PowerUpStore>().addPowerup(jet);s
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        timer.GetComponent<Timer>().BoostTime();
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}