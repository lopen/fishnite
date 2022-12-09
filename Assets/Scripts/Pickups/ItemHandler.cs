using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public float removeTime = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, removeTime);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime);
    }
}
