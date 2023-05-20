using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GeneratePosition : MonoBehaviour
{
    private float Xpos, Zpos;
    
    // Start is called before the first frame update
    void Start()
    {
        Xpos = Random.Range(-5, 5);
        Zpos = Random.Range(-5, 5);

        //gameObject.transform.position = new Vector3(Xpos, -2, Zpos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
