using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRelyOnTroyCoin : MonoBehaviour
{
    private string _troyCoinGrabbedEvent = "pItem_hasGrabbedTroyCoin";
    // Start is called before the first frame update
    void Start()
    {
        _troyCoinGrabbedEvent += UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (PlayerPrefsManager.HasPlayerPrefBeenActivated(_troyCoinGrabbedEvent))
        {
            Destroy(gameObject);
            return;
        }

    }
    
}
