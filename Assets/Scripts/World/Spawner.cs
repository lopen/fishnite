using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject obstacle;

    List<GameObject> pos = new List<GameObject>();

    public GameObject fishingSpot;

    public List<GameObject> powerups;

    public float spawnRate = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnObstacles();
        StartCoroutine("spawnItems");
    }

    void spawnObstacles() 
    {
        for (int i = 0; i < Random.Range(8, 15); i++) 
        {
            pos.Add(Instantiate(obstacle, getPos(false), new Quaternion(0,Random.Range(0,180),0,0)));
        }
    }

    IEnumerator spawnItems()
    {

        while (true) 
        {
            Instantiate(powerups[Random.Range(0, powerups.Count)], getPos(true), Quaternion.identity);
            Instantiate(powerups[Random.Range(0, powerups.Count)], getPos(true), Quaternion.identity);
            Instantiate(fishingSpot, getPos(true), Quaternion.identity);
            Instantiate(fishingSpot, getPos(true), Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);  
        }
    }

    Vector3 getPos(bool item) 
    {
        bool foundPos = false;
        Vector3 pos = new Vector3(0,0,0);

        while (!foundPos)
        {
            pos = new Vector3(
                            Random.Range(10, 85),
                            item ? 1 : Random.Range(-5, -3),
                            Random.Range(-3, -85));

            foundPos = validatePos(pos);
        }

        return pos; 
    }

    bool validatePos(Vector3 newPos)
    {
        foreach (GameObject obj in pos)
        {
            Vector3 objPos = obj.transform.position;
            if ((objPos.x + 10 < newPos.x && newPos.x < objPos.x - 10) ||
                (objPos.z + 10 < newPos.z && newPos.z < objPos.z - 10))
            {
                return false;
            }
        }
        return true;
    }
}
