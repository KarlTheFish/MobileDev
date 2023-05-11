using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public int maxDistance = 70;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Currently using eventID is useless, but it can be used to implement showing different scenes
    public void ActivateEvent(int eventID)
    {
        if (eventID == 1)
        {
            SceneManager.LoadScene("Fishing");
        } else if (eventID == 2)
        {
            SceneManager.LoadScene("Fishing");
        }
    }
}
