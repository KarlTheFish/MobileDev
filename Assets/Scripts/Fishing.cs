using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    Vector3 rot;

    private bool FishHooked;
    private int FishPulling;
    private GameObject FishObject;

    // Start is called before the first frame update
    void Start()
    {
        rot = Vector3.zero;
        Input.gyro.enabled = true;
        StartCoroutine(FishingProcess());
    }

    // Update is called once per frame
    void Update()
    {
        rot.x = -10 * (Input.gyro.rotationRateUnbiased.x);
        if (rot.x < -25 && FishHooked == true)
        {
            Debug.Log("PULLING FISH");
            StartCoroutine(FishGone());
        }
        gameObject.transform.Rotate(Vector3.right, rot.x);
    }

    void FishGot()
    {
        Debug.Log("YOU GOT A FISH!");
    }

    IEnumerator FishingProcess() {
        while (FishHooked == false) {
            FishPulling = Random.Range(0, 100);
            if(FishPulling > 50) {
                if (FishPulling < 70) {
                    FishObject = Instantiate(Resources.Load("Fish/fish1") as GameObject);
                }
                else {
                    if (FishPulling < 90) {
                        FishObject = Instantiate(Resources.Load("Fish/fish2") as GameObject);
                    }
                    else {
                        FishObject = Instantiate(Resources.Load("Fish/fish3") as GameObject);
                    }
                }
                FishHooked = true;
            }
            else {
                FishObject.Destroy();
                Debug.Log("No fish got");
                FishHooked = false;
            }
            yield return new WaitForSeconds(2);
        }

        if (FishHooked == true)
        {
            StopCoroutine(FishingProcess());
        }
    }

    IEnumerator FishGone()
    {
        yield return new WaitForSeconds(1);
        FishObject.Destroy();
        FishHooked = false;
        StartCoroutine(FishingProcess());
    }
}
