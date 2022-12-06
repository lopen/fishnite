using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehaviour : MonoBehaviour
{
    private Transform PlayerPos;
    private float RotationSpeed = 3.0f;
    private float ChaseSpeed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(PlayerPos.position - transform.position), RotationSpeed * Time.deltaTime);
        transform.position += transform.forward * ChaseSpeed * Time.deltaTime;
    }
}
