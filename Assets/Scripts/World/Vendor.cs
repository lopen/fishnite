using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    private bool canSell = false;
    private GameObject player;
    private Score score;
    public AudioClip sellSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        score = GameObject.FindWithTag("Score").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSell) {  
            if (Input.GetKeyDown("space")) {
                // sell fish in inventory
                Debug.Log("SELLING ALL YOUR FISH O H NO");
                score.IncreaseScore();
                AudioSource.PlayClipAtPoint(sellSound, transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            canSell = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            canSell = false;
        }
    }
}
