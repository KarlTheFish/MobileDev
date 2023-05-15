using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    Vector3 rot;

    public int localScore;
    public int number;

    private bool FishHooked;
    private int FishPulling;
    private GameObject FishObject;

    // Start is called before the first frame update
    void Start()
    {
        rot = Vector3.zero;
        Input.gyro.enabled = true;
        StartCoroutine(FishingProcess());
        GetGlobalScore();
    }

    public void GetGlobalScore()
    {
        number = AuthManager.instance.globalScore;
    }

    public void CompareScore()
    {
        if (number < localScore)
        {
            StartCoroutine(AuthManager.instance.UpdateScore(localScore));
        }
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
                    localScore = Random.Range(1, 40);
                }
                else {
                    if (FishPulling < 90) {
                        FishObject = Instantiate(Resources.Load("Fish/fish2") as GameObject);
                        localScore = Random.Range(40, 80);
                    }
                    else {
                        FishObject = Instantiate(Resources.Load("Fish/fish3") as GameObject);
                        localScore = Random.Range(80, 100);
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
            CompareScore();
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
