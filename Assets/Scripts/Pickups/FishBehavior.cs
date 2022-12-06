using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    [SerializeField] private GameObject splashObject;

    private Rigidbody rb;
    public float jumpForce = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float waveHeight = WaveManager.instance.getWaveHeight(transform.position.x, transform.position.z);

        if (transform.position.y < waveHeight) {
            rb.AddForce(new Vector3(0,1,0) * 3 * Time.deltaTime);
        }
    }
}
