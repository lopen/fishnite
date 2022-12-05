using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSplash : MonoBehaviour
{
    [SerializeField] private GameObject splashObject;
    private Vector3 fishBig;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localEulerAngles.z == 160) {
            fishBig = this.transform.position;
        }
    }

    private void OnDestroy() {
        GameObject fishTest = Instantiate(splashObject) as GameObject;
        fishTest.transform.localPosition = fishBig;
        Destroy(fishTest, 2f);
    }
}
