using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    Vector3 rot;
    // Start is called before the first frame update
    void Start()
    {
        rot = Vector3.zero;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        rot.x = -10 * (Input.gyro.rotationRateUnbiased.x);
        //Debug.Log(Input.gyro.attitude);
        transform.Rotate(rot);
    }
}
