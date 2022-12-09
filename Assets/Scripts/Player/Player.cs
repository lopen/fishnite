using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else if (instance != this) 
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update() { }
}
