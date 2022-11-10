using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public int floaterCounter = 4;
    public float waterDrag = 2f;
    public float waterADrag = 4f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.AddForceAtPosition(Physics.gravity / floaterCounter, transform.position, ForceMode.Acceleration);

        float waveHeight = WaveManager.instance.getWaveHeight(transform.position.x, transform.position.z);
        if (transform.position.y < waveHeight) 
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rigidbody.AddForce(displacementMultiplier * -rigidbody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidbody.AddTorque(displacementMultiplier * -rigidbody.angularVelocity * waterADrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
