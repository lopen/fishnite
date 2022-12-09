using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehaviour : MonoBehaviour
{
    private Transform PlayerPos;
    private float RotationSpeed = 3.0f;
    private float ChaseSpeed = 3.0f;
    public bool sharkAttacking;
    public bool sharkActive;

    [SerializeField] private GameObject searchIndicator;
    [SerializeField] private GameObject attackIndicator;

    [SerializeField] private Animator sharkAnims;
    private MeshCollider sharkCollider;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        sharkAttacking = false;
        sharkActive = true;
        sharkCollider = this.GetComponent<MeshCollider>();
        searchIndicator.SetActive(true); 

        StartCoroutine(SharkBH());
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Co-routine for shark behaviour
    IEnumerator SharkBH () {
        while (sharkActive == true) {
            var distanceCheck = Vector3.Distance(PlayerPos.position, transform.position);

            if (sharkAttacking == false) {
                transform.Rotate(0, 20f * Time.deltaTime, 0);
                transform.position += transform.forward * Time.deltaTime * ChaseSpeed;
            } 
            
            if (distanceCheck < 30f) {
                sharkAttacking = true;
                print("shark ANGRY");
                searchIndicator.SetActive(false);
                attackIndicator.SetActive(true);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(PlayerPos.position - transform.position), RotationSpeed * Time.deltaTime);
                transform.position += transform.forward * ChaseSpeed * Time.deltaTime;
            } else {
                sharkAttacking = false;
                attackIndicator.SetActive(false);
                searchIndicator.SetActive(true);
            }

            yield return new WaitForSeconds(0.1f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            sharkAnims.SetBool("Attack", true);
            this.GetComponent<AudioSource>().Play();
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Player") {
            sharkAnims.SetBool("Moving", true);
            sharkAnims.SetBool("Attack", false);
        }
    }
}
