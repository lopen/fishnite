using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    // Rigidbody to apply force to (boat)
    new private Rigidbody rigidbody;

    public float depthBeforeSubmerged = 1f; // Variable for depth before submerging
    public float displacementAmount = 3f; // Amount to submerge
    public int floaterCounter = 4; // Count of float
    public float waterDrag = 2f; // Drag in water
    public float waterADrag = 4f; // Drag in water

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.parent.GetComponent<Rigidbody>();       
    }

    // FixedUpdate is called once per frame at end of frame
    void FixedUpdate()
    {
        // Apply gravity on rigibody, diveded by floaterCount so each point is adding the same amount
        rigidbody.AddForceAtPosition(Physics.gravity / floaterCounter, transform.position, ForceMode.Acceleration);

        // Get the wave height of the current xy coordinates of the floater point
        float waveHeight = WaveManager.instance.getWaveHeight(transform.position.x, transform.position.z);
        // If current y pos is below waveHeight then we apply bouancy
        if (transform.position.y < waveHeight) 
        {
            // Calculate the displacement multiplier
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;

            // Apply force
            rigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rigidbody.AddForce(displacementMultiplier * -rigidbody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidbody.AddTorque(displacementMultiplier * -rigidbody.angularVelocity * waterADrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
