using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    // rigibody to apply force to
    private Rigidbody rigidbody;

    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public int floaterCounter = 4;
    public float waterDrag = 2f;
    public float waterADrag = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.parent.GetComponent<Rigidbody>();       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // apply gravity on rigibody, diveded by floaterCount so each point is adding the same amount
        rigidbody.AddForceAtPosition(Physics.gravity / floaterCounter, transform.position, ForceMode.Acceleration);

        // get the wave height of the current xy coordinates of the floater point
        float waveHeight = WaveManager.instance.getWaveHeight(transform.position.x, transform.position.z);
        // if current y pos is below waveHeight then we apply bouancy
        if (transform.position.y < waveHeight) 
        {
            // calculate the displacement multiplier
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;

            // apply force
            rigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rigidbody.AddForce(displacementMultiplier * -rigidbody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidbody.AddTorque(displacementMultiplier * -rigidbody.angularVelocity * waterADrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
