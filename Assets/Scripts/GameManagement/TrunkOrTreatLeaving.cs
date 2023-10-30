using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkOrTreatLeaving : MonoBehaviour
{
    string _playerTrippedEventName = "p_playerHasTripped";
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefsManager.HasPlayerPrefBeenActivated(_playerTrippedEventName))
        {
            Invoke("UnlockScene", 0.1f);
        }
        
    }
    void UnlockScene()
    {
        SceneLocker sceneLocker = GameObject.Find("Scene Manager").GetComponent<SceneLocker>();
        sceneLocker.IsLocked = false;
    }
}
