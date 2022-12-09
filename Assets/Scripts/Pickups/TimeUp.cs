using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUp : MonoBehaviour
{
    private Timer timer;
    public AudioClip collectSound;

    void Start()
    {
        timer = Timer.instance;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            timer.BoostTime();
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}