using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    //make this a singleton
    public static PlayerPrefsManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    static void CreatePlayerPref(string playerPrefName)
    {
        if (!PlayerPrefs.HasKey(playerPrefName))
        {
            PlayerPrefs.SetInt(playerPrefName, 0);
        }
    }

    static bool PlayerPrefStatus(string playerPrefName)
    {
        if (!PlayerPrefs.HasKey(playerPrefName))
        {
            PlayerPrefs.SetInt(playerPrefName, 0);
        }
        
        if (PlayerPrefs.GetInt(playerPrefName) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
