using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public float removeTime = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("Flicker"); have the item flicker before it despawns
        transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime);
        Destroy(gameObject, removeTime);
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
