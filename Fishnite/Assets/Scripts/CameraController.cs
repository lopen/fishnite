using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject boat;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = boat.transform.position - transform.position;
    }

    void LateUpdate() 
    {
        float desiredAngle = boat.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = boat.transform.position - (rotation * offset);
        //transform.LookAt(boat.transform);
        //transform.position = boat.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
