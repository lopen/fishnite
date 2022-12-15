using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public float removeTime = 20.0f; // Defines item/powerup remove time

    // Start is called before the first frame update / Destroys initial objects following remove period
    void Start()
    {
        Destroy(gameObject, removeTime);
    }

    // Handles animations of powerups/pickups
    void Update()
    {
        transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime);
    }
}
