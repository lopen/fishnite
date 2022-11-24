using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{

    // public Dictionary<Vector3, Chunk> chunks;
    // public GameObject player;
    // public float renderDistance = 50f;

    // Start is called before the first frame update
    void Start()
    {
        // chunks = new Dictionary<Vector3, Chunk>();
        // loadChunk();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 playerPos = player.transform.position;
        // foreach (KeyValuePair<Vector3, Chunk> chunk in chunks)
        // {
        //     if (chunk.Value.loaded == false && Vector3.Distance(chunk.Key, playerPos) < renderDistance)
        //     {
        //         load(chunk.Value);
        //     } else if (chunk.Value.loaded)
        //     {
        //         unload(chunk.Value);
        //     }
        // }
    }

    public void populateChunks() 
    {

    }

    void loadChunk() 
    {
        // chunks.Add(new Vector3(-15,0,-15), Instantiate(oceanChunk, new Vector3(-15,0,-15), new Quaternion(0,0,0,0))); // 0 1 0
        // chunks.Add(new Vector3(-15,0,-45), Instantiate(oceanChunk, new Vector3(-15,0,-45), new Quaternion(0,0,0,0))); // 0 1 0
        // chunks.Add(new Vector3(-15,0,15), Instantiate(oceanChunk, new Vector3(-15,0,15), new Quaternion(0,0,0,0)));  // 0 1 0

        // //Instantiate(oceanChunk, new Vector3(-45,0,-15), new Quaternion(0,0,0,0)); // 1 0 0
        // //Instantiate(oceanChunk, new Vector3(-45,0,-45), new Quaternion(0,0,0,0)); // 1 0 0
        // //Instantiate(oceanChunk, new Vector3(-45,0,15), new Quaternion(0,0,0,0));  // 1 0 0

        // chunks.Add(new Vector3(-45,0,-15), Instantiate(oceanChunk, new Vector3(-15,0,-15), new Quaternion(0,0,0,0))); // 0 1 0
        // chunks.Add(new Vector3(-45,0,-45), Instantiate(oceanChunk, new Vector3(-15,0,-45), new Quaternion(0,0,0,0))); // 0 1 0
        // chunks.Add(new Vector3(-45,0,15), Instantiate(oceanChunk, new Vector3(-15,0,15), new Quaternion(0,0,0,0)));  // 0 1 0

        // chunks.Add(new Vector3(15,0,-15), Instantiate(oceanChunk, new Vector3(-15,0,-15), new Quaternion(0,0,0,0))); // 0 1 0
        // chunks.Add(new Vector3(15,0,-45), Instantiate(oceanChunk, new Vector3(-15,0,-45), new Quaternion(0,0,0,0))); // 0 1 0
        // chunks.Add(new Vector3(15,0,15), Instantiate(oceanChunk, new Vector3(-15,0,15), new Quaternion(0,0,0,0)));  // 0 1 0


        // //Instantiate(oceanChunk, new Vector3(15,0,-15), new Quaternion(0,0,0,0));  // 0 0 1
        // //Instantiate(oceanChunk, new Vector3(15,0,-45), new Quaternion(0,0,0,0));  // 0 0 1
        // //Instantiate(oceanChunk, new Vector3(15,0,15), new Quaternion(0,0,0,0));   // 0 0 1


    }

        // void load(Chunk chunk) 
        // {
        //     chunk.oceanTile.SetActive(true);
        //     chunk.loaded = true;
        // }

        // void unload(Chunk chunk) 
        // {
        //     chunk.oceanTile.SetActive(false);
        //     chunk.loaded = false;
        // }
}

// public struct Chunk {
//     public GameObject oceanTile;
//     public bool loaded = false;
//     // terrain
//     // pickups
//     // minigames
// }
