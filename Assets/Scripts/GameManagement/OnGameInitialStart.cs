using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnGameInitialStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if player prefs exist load the load from previous menu
        if (PlayerPrefs.HasKey("p_inventory"))
        {
            //load the previous scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoadPreviousMenu");
        }
        else
        {
            //load the first scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }

   
}
