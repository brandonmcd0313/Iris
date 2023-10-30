using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveSceneManager : MonoBehaviour
{
    string _shouldLoadPreviousActive = "p_shouldLoadPreviousActiveScene";
    static ActiveSceneManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //destroy this gameobject
            Destroy(this.gameObject);
        }

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(PlayerPrefsManager.HasPlayerPrefBeenActivated(_shouldLoadPreviousActive))
        {
            //disbale the player pref
            PlayerPrefsManager.DeactivatePlayerPref(_shouldLoadPreviousActive);
            string sceneName = PlayerPrefsManager.GetActiveSceneName();
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            return;
        
        }

        PlayerPrefsManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
    }
}
