using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatController : MonoBehaviour
{
    public float speed = 11f; 
    public float turnSpeed = 4f;
    public float boostMultiplier = 1.6f;

    public float nitrusMeter = 0f; // essentially just how many seconds of nitrus you have
    public float maxNitrus = 4f;
    private bool boosting = false;

    private Image nitrusM;
    [SerializeField] private GameObject boatSmoke;
    [SerializeField] private GameObject boatAfterburner;

    [SerializeField] private AudioSource afterburnAudio;
    [SerializeField] private AudioSource crashAudio;
    [SerializeField] private AudioSource hornAudio;
    [SerializeField] private AudioSource engineAudio;
    [SerializeField] private AudioSource waterAudio;
 
    Rigidbody boatRigidbody;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        boatRigidbody = GetComponent<Rigidbody>();
        nitrusM = GameObject.FindGameObjectWithTag("NitrusMeter").GetComponent<Image>();
    }

    void Update()
    {
        // check if player is hitting nitrus button, if boosting then start nitrus
        if (Input.GetKeyDown(KeyCode.LeftShift) && !boosting && nitrusMeter > 0) {
            //StartCoroutine(decreaseNitrusMeter(nitrusMeter));
            StartCoroutine(increaseSpeed(nitrusMeter));
        }
        if (boosting)
        {
            nitrusM.fillAmount -= 0.25f * Time.deltaTime;
        }
        if (Input.GetKeyDown("h") && !hornAudio.isPlaying) {
            hornAudio.Play();
        }
        
        if(GetComponent<Rigidbody>().velocity.magnitude > 0) {
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
            nitrusM.fillAmount += 0.25f;
            nitrusMeter++;
        }
    }

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

    void OnCollisionEnter(Collision collision)
    {
        playerHealth.decreaseHealth();
        if (!crashAudio.isPlaying)
        {
            crashAudio.Play();
        }
    }
}
