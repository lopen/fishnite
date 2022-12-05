using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject obstacle;
    //Vector3[] obstaclePos;
    
    public GameObject itemTime;
    public GameObject itemPower;
    public GameObject fishingSpot;

    public float spawnRate = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnObstacles();
        StartCoroutine("spawnItems");
    }

    void spawnObstacles() 
    {
        for (int i = 0; i < Random.Range(5, 10); i++) 
        {
            Instantiate(obstacle, getPos(false), Quaternion.identity);
        }
    }

    IEnumerator spawnItems()
    {

        while (true) 
        {
            Instantiate(itemTime, getPos(true), Quaternion.identity);
            Instantiate(itemPower, getPos(true), Quaternion.identity); 
            Instantiate(fishingSpot, getPos(true), Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);  
        }
    }

    Vector3 getPos(bool item) 
    {
        Vector3 pos = new Vector3(
                                Random.Range(10, 85), 
                                item ? 1 : Random.Range(-5,-3), 
                                Random.Range(-3, -85));

        // for (int i = 0; i < obstaclePos.Length; i++) 
        // {
        //     Vector3 p = obstaclePos[i];
        //     if (p.x == pos.x || p.z == pos.z) {

        //     }
        // }


        // obstaclePos[obstaclePos.Length] = pos;

        return pos; 
    }
}
