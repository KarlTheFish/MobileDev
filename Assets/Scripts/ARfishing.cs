using UnityEngine;

public class ARfishing : MonoBehaviour
{
    private GameObject FishingRod;
    
    void Start()
    {
        FishingRod = GameObject.Find("FishRod");
    }
    private void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch (you can modify this based on your requirements)

            if (touch.phase == TouchPhase.Began)
            {
                // Raycast to detect if the touch is on this object
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        // Object is touched
                        HandleTouch();
                    }
                }
            }
        }
    }

    // This method will be called when the object is touched
    private void HandleTouch()
    {
        // Add your custom logic here
    }
}
