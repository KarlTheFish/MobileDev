using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARcursor : MonoBehaviour
{
    public ARRaycastManager raycastManager; // Detects the planes we touch
    public GameObject Lake; // The Lake game object

    private GameObject UItext;
    
    private bool LakePlaced = false; // Tracks if the lake is already placed

    void Start()
    {
        UItext = GameObject.Find("Text");
    }

    void Update()
    {
        if (!LakePlaced)
        {
            PlaceLakeOnPlane();
        }
        if (LakePlaced)
        {
            UItext.SetActive(false);
        }
    }

    void PlaceLakeOnPlane()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2f, Screen.height / 2f), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            int planeIndex = Random.Range(0, hits.Count);
            Vector3 position = hits[planeIndex].pose.position;
            Quaternion rotation = hits[planeIndex].pose.rotation;
            GameObject.Instantiate(Lake, position, rotation);
            LakePlaced = true;
        }
    }
}
