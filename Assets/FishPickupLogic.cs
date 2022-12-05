using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPickupLogic : MonoBehaviour
{

    [SerializeField] private GameObject fish1;
    [SerializeField] private GameObject fish2;

    [SerializeField] private GameObject splashObject;
    private Vector3 fishBig;

    [SerializeField] private GameObject minigame;
    private bool canFish = false;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    //Update is called once per frame
    void Update()
    {
        if (canFish) {  
            if (Input.GetKeyDown("f")) {
                Instantiate(minigame, (player.transform.position - player.transform.forward * 3 + player.transform.up * 2), player.transform.rotation);
                Destroy(gameObject);
            }
        }

        if (fish1.transform.localEulerAngles.z == 160) {
            fishBig = fish1.transform.position;
            GameObject fishTest = Instantiate(splashObject) as GameObject;


            fishTest.transform.localPosition = fishBig;
            Destroy(fishTest, 10f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            canFish = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            canFish = false;
        }
    }
}