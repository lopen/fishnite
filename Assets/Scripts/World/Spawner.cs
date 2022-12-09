using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // position of obstacles, used to make sure powerups and fishingspots
    // don't spawn ontop of rock terrain
    List<GameObject> pos = new List<GameObject>();

    // GameObjects to be spawned 
    // obstacles = large rock terrain, 
    // fishingSpot = trigger object for activating minigame
    // powerups = list of different powerup prefabs
    public GameObject obstacle;
    public GameObject fishingSpot;
    public List<GameObject> powerups;

    // how often we spawn a new set of items
    public float spawnRate = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnObstacles();
        StartCoroutine("spawnItems");
    }

    // spawn initial rock terrain
    void spawnObstacles() 
    {
        for (int i = 0; i < Random.Range(8, 15); i++) 
        {
            pos.Add(Instantiate(obstacle, getPos(false), new Quaternion(0,Random.Range(0,180),0,0)));
        }
    }

    // Spawns two random powerups and two fishingspots per spawnRate (seconds)
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

    // method to get a new Vector3 for object spawn
    // takes bool to determine if item or not
    // getPos spawns obstacles underwater as they are so large
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

    // checks if a vector is near rock terrain obstacles
    bool validatePos(Vector3 newPos)
    {
        foreach (GameObject obj in pos)
        {
            Vector3 objPos = obj.transform.position;
            if ((objPos.x + 2 > newPos.x && newPos.x > objPos.x - 2) ||
                (objPos.z + 2 > newPos.z && newPos.z > objPos.z - 2))
            {
                return false;
            }
        }
        return true;
    }
}
