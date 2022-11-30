using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 5f; 
    public float turnSpeed = 3f;
    public float topSpeed = 10f;

    Vector3 movement;
    Vector3 turnMovement;
    Rigidbody boatRigidbody;
    //Rigidbody rudderRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        boatRigidbody = GetComponent<Rigidbody>();
        //rudderRigidbody = GameObject.FindWithTag("Rudder").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float lr = Input.GetAxisRaw("Horizontal"); // left right, lb
        float fb = Input.GetAxisRaw("Vertical");   // forward backward, fb
       
        if (fb != 0)
        {
            move(fb);
        }
        
        if (lr != 0)
        {
            turn(lr);
        }
    }

    void move(float fb)
    {
        boatRigidbody.AddForce(this.transform.forward * fb * speed);
    }

    void turn(float lr)
    {
        turnMovement.Set(lr, 0f, 0f);
        boatRigidbody.AddTorque(this.transform.up * turnSpeed  * lr);
    }
}