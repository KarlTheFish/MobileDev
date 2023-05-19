using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARcursor : MonoBehaviour
{
    public GameObject cursorChildObject; //The cursor image
    public GameObject objectToPlace; //The 3d object we'll be placing(Fish lake)

    public ARRaycastManager raycastManager; //Detects the planes we touch

    public bool useCursor = true; //Whether you can see the cursor or not

    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(useCursor);
    }

    // Update is called once per frame
    void Update()
    {
        if (useCursor)
        {
            updateCursor();
        }

        //Checking if the screen was touched for stuff
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (useCursor)
            {
                GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits,
                    UnityEngine.XR.ARSubsystems.TrackableType.Planes); // Raycast from the screen to the planes? not sure what this actually does
                if (hits.Count > 0)
                {
                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                }
            }
        }
    }

    void updateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f)); //Center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>(); //List of all the planes we touch
        raycastManager.Raycast(screenPosition, hits,
            UnityEngine.XR.ARSubsystems.TrackableType.Planes); //Raycast from the center of the screen to the planes

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position; //Move the cursor to the position of the plane we touch
            transform.rotation = hits[0].pose.rotation; //Rotate the cursor to the rotation of the plane we touch
        }
    }

}
