using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        genObstacles();
    }

    void genObstacles() 
    {
        for (int i = 0; i < Random.Range(3, 10); i++) 
            {
                Instantiate(obstacle, getPos(), new Quaternion(0,Random.Range(0,360),0,0));
            }
    }

    Vector3 getPos() 
    {
        return new Vector3(Random.Range(10, 85), Random.Range(-5,-3), Random.Range(-3, -85));
    }
}
