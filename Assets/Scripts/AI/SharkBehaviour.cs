using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehaviour : MonoBehaviour
{
    private Transform PlayerPos; // Player position
    private float RotationSpeed = 3.0f; // Speed of turning
    private float ChaseSpeed = 3.0f; // Speed of chase
    public bool sharkAttacking; // Shark current attack status, used for setting indication
    public bool sharkActive; // Shark active status (for running enumerator)

    [SerializeField] private GameObject searchIndicator; // Indicator for searching status
    [SerializeField] private GameObject attackIndicator; // Indicator for attackingstatus

    [SerializeField] private Animator sharkAnims; // Animations for shark (moving & attack)
    private MeshCollider sharkCollider; // Shark mesh collider

    // Start is called before the first frame update / Handles variable assignment
    void Start()
    {
        PlayerPos = Player.instance.transform;
        sharkAttacking = false;
        sharkActive = true;
        sharkCollider = this.GetComponent<MeshCollider>();
        searchIndicator.SetActive(true); 

        StartCoroutine(SharkBH());
    }

    // Co-routine for shark behaviour / Handles calculations for distance, status indication and chase calculation
    IEnumerator SharkBH () {
        while (sharkActive == true) {
            var distanceCheck = Vector3.Distance(PlayerPos.position, transform.position);

            if (sharkAttacking == false) {
                transform.Rotate(0, 20f * Time.deltaTime, 0);
                transform.position += transform.forward * Time.deltaTime * ChaseSpeed;
            } 
            
            if (distanceCheck < 30f) {
                sharkAttacking = true;
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

    // Called on collision enter, sets shark status, audio output and animation
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            sharkAnims.SetBool("Attack", true);
            this.GetComponent<AudioSource>().Play();
        }
    }

    // Called on collision exit, sets shark status & animation
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Player") {
            sharkAnims.SetBool("Moving", true);
            sharkAnims.SetBool("Attack", false);
        }
    }
}
