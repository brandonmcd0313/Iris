using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TroyCoinSpawnManager : MonoBehaviour
{
    private string _troyCoinAbility = "p_canSpawnTroyCoin";
    //items are added to the inventory
    private string _troyCoinGrabbedEvent = "pItem_hasGrabbedTroyCoin";
    [SerializeField] GameObject _troyCoinPrefab;
    [SerializeField] Vector2 _troyCoinSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
       _troyCoinGrabbedEvent += UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        
        if(PlayerPrefsManager.HasPlayerPrefBeenActivated(_troyCoinGrabbedEvent))
        {
            return;
        }


        if (PlayerPrefsManager.HasPlayerPrefBeenActivated(_troyCoinAbility))
        {
            Instantiate(_troyCoinPrefab, _troyCoinSpawnPoint, Quaternion.identity);
        }
    }

    public void OnTroyCoinGrab()
    {
        PlayerPrefsManager.ActivatePlayerPref(_troyCoinGrabbedEvent);
    }

}
