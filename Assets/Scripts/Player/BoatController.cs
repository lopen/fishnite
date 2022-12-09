using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatController : MonoBehaviour
{
    // boat movement variables
    public float speed = 11f; 
    public float turnSpeed = 4f;
    public float boostMultiplier = 1.6f;

    // nitrus variables 
    public float nitrusMeter = 0f;  // essentially just how many seconds of nitrus you have
    public float maxNitrus = 4f;    // max amount of nitrus possible to collect 
    private bool boosting = false;  // if the player is boosting / using nitrus
    private Image nitrusBar;          // the nitrus bar GUI element

    // boat smoke and nitrus afterburner prefabs
    [SerializeField] private GameObject boatSmoke;
    [SerializeField] private GameObject boatAfterburner;

    // audio itmes
    [SerializeField] private AudioSource afterburnAudio;
    [SerializeField] private AudioSource crashAudio;
    [SerializeField] private AudioSource hornAudio;
    [SerializeField] private AudioSource engineAudio;
    [SerializeField] private AudioSource waterAudio;
    
    // boat rigidbody
    Rigidbody boatRigidbody;

    // player health
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        boatRigidbody = GetComponent<Rigidbody>();
        nitrusBar = GameObject.FindGameObjectWithTag("NitrusMeter").GetComponent<Image>();
    }

    void Update()
    {
        // check if player is hitting nitrus button, !boosting and nitrus > 0 then run nitrus meter down
        if (Input.GetKeyDown(KeyCode.LeftShift) && !boosting && nitrusMeter > 0) {
            StartCoroutine(increaseSpeed(nitrusMeter));
        }
        // if boosting then decrease fillAmount of nitrus bar over time
        if (boosting)
        {
            nitrusBar.fillAmount -= 0.25f * Time.deltaTime;
        }
        // honk the horn
        if (Input.GetKeyDown("h") && !hornAudio.isPlaying) {
            hornAudio.Play();
        }
        // boat audio    
        if(boatRigidbody.velocity.magnitude > 0) {
            if(!engineAudio.isPlaying) {
                engineAudio.Play();
            }

            if(!waterAudio.isPlaying) {
                waterAudio.Play();
            }
        }
    }

    void FixedUpdate()
    {
        // movement
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
        boatRigidbody.AddTorque(this.transform.up * turnSpeed  * lr);
    }

    public void addNitrus()
    {
        // add nitrus to our nitrus counter
        if (nitrusMeter < maxNitrus) {
            nitrusBar.fillAmount += 0.25f;
            nitrusMeter++;
        }
    }

    // change player turn and movespeed and activate afterburner prefab
    IEnumerator increaseSpeed(float nitrus) 
    {
        boosting = true;
        speed = speed * boostMultiplier;
        turnSpeed = turnSpeed * boostMultiplier;
        boatAfterburner.SetActive(true);
        afterburnAudio.Play();
        boatSmoke.GetComponent<ParticleSystem>().Pause();
        yield return new WaitForSeconds(nitrus);
        boosting = false;
        nitrusMeter = 0f; // we just use up all nitrus when a button is pressed
        speed = speed / boostMultiplier;
        turnSpeed = turnSpeed / boostMultiplier;
        boatAfterburner.SetActive(false);
        boatSmoke.GetComponent<ParticleSystem>().Play();
    }

    // if the player crashes into anything they will lose health
    void OnCollisionEnter(Collision collision)
    {
        playerHealth.decreaseHealth();
        if (!crashAudio.isPlaying)
        {
            crashAudio.Play();
        }
    }
}
