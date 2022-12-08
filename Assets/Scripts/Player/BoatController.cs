using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 11f; 
    public float turnSpeed = 4f;
    public float boostMultiplier = 1.6f;

    public float nitrusMeter = 0f; // essentially just how many seconds of nitrus you have
    public float maxNitrus = 4f;
    private bool boosting = false;

    Rigidbody boatRigidbody;

    GameObject[] leftFloaters;
    GameObject[] rightFloaters;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        boatRigidbody = GetComponent<Rigidbody>();
        leftFloaters = GameObject.FindGameObjectsWithTag("Left");
        rightFloaters = GameObject.FindGameObjectsWithTag("Right");
    }

    void Update()
    {
        // check if player is hitting nitrus button, if boosting then start nitrus
        if (Input.GetKeyDown("n") && !boosting) {
            StartCoroutine(increaseSpeed(nitrusMeter));
        }
    }

    void FixedUpdate()
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
        //turnMovement.Set(lr, 0f, 0f);
        boatRigidbody.AddTorque(this.transform.up * turnSpeed  * lr);
    }

    public void addNitrus()
    {
        // add nitrus to our nitrus counter
        if (nitrusMeter < maxNitrus) {
            nitrusMeter++;
        }
    }

    IEnumerator increaseSpeed(float nitrus) 
    {
        // Reember to enable the nitrus booster on the boat
        boosting = true;
        speed = speed * boostMultiplier;
        turnSpeed = turnSpeed * boostMultiplier;
        yield return new WaitForSeconds(nitrus);
        boosting = false;
        nitrusMeter = 0f; // we just use up all nitrus when a button is pressed
        speed = speed / boostMultiplier;
        turnSpeed = turnSpeed / boostMultiplier;
    }

    void OnCollisionEnter(Collision collision)
    {
        playerHealth.decreaseHealth();
    }
}
