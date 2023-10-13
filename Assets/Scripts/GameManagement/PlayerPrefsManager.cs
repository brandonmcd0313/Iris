using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    //make this a singleton
    public static PlayerPrefsManager instance;

    /* PREFIX
     *  all player prefs start with p_
     */

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
    public static void CreatePlayerPref(string playerPrefName)
    {
        if (!PlayerPrefs.HasKey(playerPrefName))
        {
            PlayerPrefs.SetInt(playerPrefName, 0);
        }
    }

    public static bool HasPlayerPrefBeenActivated(string playerPrefName)
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

    public static void ActivatePlayerPref(string playerPrefName)
    {
        if (!PlayerPrefs.HasKey(playerPrefName))
        {
            PlayerPrefs.SetInt(playerPrefName, 0);
        }

        PlayerPrefs.SetInt(playerPrefName, 1);
    }
}
