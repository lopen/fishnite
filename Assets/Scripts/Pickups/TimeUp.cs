using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUp : MonoBehaviour
{
    private Timer timer;
    public AudioClip collectSound; // Defines pickup sound

    // Start is called before the first frame update / Gets timer instance
    void Start()
    {
        timer = Timer.instance;
    }

    // On trigger enter / Used to detect player pickup
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            timer.BoostTime();
            Destroy(gameObject);
        }
    }
}