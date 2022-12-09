using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // boat object
    public GameObject boat;
    // camera offset
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // enable depth texture for shader
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;

        // setup camera position
        transform.position = new Vector3(boat.transform.position.x, boat.transform.position.y + 5, boat.transform.position.z - 10);
        transform.rotation = Quaternion.Euler(15, 0, 0);
        offset = boat.transform.position - transform.position;
    }

    void LateUpdate() 
    {
        // rotate camera behind boat 
        float desiredAngle = boat.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = boat.transform.position - (rotation * offset);// - offset;
        transform.LookAt(boat.transform);
    }

    // Update is called once per frame
    void Update() { }
}
