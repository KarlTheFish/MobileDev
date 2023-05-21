using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject Scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PressButton()
    {
        SceneManager.LoadScene("Location-basedGame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
