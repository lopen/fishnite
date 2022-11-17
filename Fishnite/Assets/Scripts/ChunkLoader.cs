using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{

    public GameObject oceanChunk;
    public GameObject player;

    private GameObject[] chunks = new GameObject[9];

    // Start is called before the first frame update
    void Start()
    {
        loadChunk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadChunk() 
    {
        Instantiate(oceanChunk, new Vector3(-15,0,-15), new Quaternion(0,0,0,0)); // 0 1 0
        Instantiate(oceanChunk, new Vector3(-15,0,-45), new Quaternion(0,0,0,0)); // 0 1 0
        Instantiate(oceanChunk, new Vector3(-15,0,15), new Quaternion(0,0,0,0));  // 0 1 0

        Instantiate(oceanChunk, new Vector3(-45,0,-15), new Quaternion(0,0,0,0)); // 1 0 0
        Instantiate(oceanChunk, new Vector3(-45,0,-45), new Quaternion(0,0,0,0)); // 1 0 0
        Instantiate(oceanChunk, new Vector3(-45,0,15), new Quaternion(0,0,0,0));  // 1 0 0

        Instantiate(oceanChunk, new Vector3(15,0,-15), new Quaternion(0,0,0,0));  // 0 0 1
        Instantiate(oceanChunk, new Vector3(15,0,-45), new Quaternion(0,0,0,0));  // 0 0 1
        Instantiate(oceanChunk, new Vector3(15,0,15), new Quaternion(0,0,0,0));   // 0 0 1
    }

    void destroyChunk()
    {

    }
}
