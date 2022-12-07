using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public float removeTime = 20.0f;
    //private float updown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("Flicker"); have the item flicker before it despawns
        Destroy(gameObject, removeTime);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime);
        //transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
    }

    IEnumerator Flicker()
    {
        yield return new WaitForSeconds (5);
        for (int i = 15; i >= 0; i--)
        {
            //GetComponent<Renderer>().enabled = false;
            //gameObject.SetActive(false);
            yield return new WaitForSeconds (1);
            //GetComponent<Renderer>().enabled = true;
            //gameObject.SetActive(true);
            yield return new WaitForSeconds (1);
        }
    }
}
