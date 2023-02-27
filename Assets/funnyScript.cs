using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funnyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         Debug.Log("Hello: " + gameObject.name);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.up, 5);
    }
}
