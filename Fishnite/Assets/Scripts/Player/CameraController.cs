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
        transform.position = new Vector3(transform.position.x, transform.position.y + 7, 0);
        transform.rotation = Quaternion.Euler(15, 0, 0);
        offset = boat.transform.position - transform.position;
    }

    void LateUpdate() 
    {
        float desiredAngle = boat.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = boat.transform.position - (rotation * offset);// - offset;
        transform.LookAt(boat.transform);
    }

    void rotateCamera(float angle)
    {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
