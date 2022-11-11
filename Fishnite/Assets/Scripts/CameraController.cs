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
        Quaternion rotation = Quaternion.Euler(-15, desiredAngle, 0);
        transform.position = boat.transform.position - (rotation * offset) - offset;
        transform.LookAt(boat.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
