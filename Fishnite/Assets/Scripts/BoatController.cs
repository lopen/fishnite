using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 3f; 
    Vector3 movement;
    Rigidbody boatRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        boatRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        move(h, v);
        turn();
    }

    void move(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;

        boatRigidbody.MovePosition(transform.position + movement);
    }

    void turn()
    {
        if (Input.GetKey(KeyCode.W))
        {
        } 
        else if (Input.GetKey(KeyCode.A)) 
        {
        }
        else if (Input.GetKey(KeyCode.S)) 
        {
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
        }
    }
}
