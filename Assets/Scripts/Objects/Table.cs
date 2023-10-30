using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    bool _playerCanInteract;
    [SerializeField] Sprite _highlightedTable;
    [SerializeField] Sprite _unhighlightedTable;
    public Action OnPlayerPaysTroyCoin;
    [SerializeField] GameObject _tableTroyCoin;
    string _hasPayedTroyCoinEvent = "p_hasPayedTroyCoin";
    // Start is called before the first frame update
    void Start()
    {
        CheckIfPlayerCanInteract();
    }


    void CheckIfPlayerCanInteract()
    {
        //player can interact if they have three troycoin
        string[] inventory = PlayerPrefsManager.GetItemsInInventory();
        int troycoinCount = 0;
        foreach (string item in inventory)
        {
            if (item == "troycoin")
            {
                troycoinCount++;
            }
        }

        if (troycoinCount >= 3)
        {
            _playerCanInteract = true;
        }
        else
        {
            _playerCanInteract = false;
        }

        if (PlayerPrefsManager.HasPlayerPrefBeenActivated(_hasPayedTroyCoinEvent))
        {
            _playerCanInteract = false;
            Instantiate(_tableTroyCoin, new Vector3(3.6f, -1.5f, 2), Quaternion.identity);
            Invoke("UnlockScene", 0.1f);
        }

    }

    void UnlockScene()
    {
        SceneLocker sceneLocker = GameObject.Find("Scene Manager").GetComponent<SceneLocker>();
        sceneLocker.IsLocked = false;
    }
    public void OnPlayerApproach()
    {
        if (!_playerCanInteract)
        {
            return;
        }
            GetComponent<SpriteRenderer>().sprite = _highlightedTable;
       
    }

    public void OnPlayerInteract()
    {
        if (!_playerCanInteract)
        {
            return;
        }
        PlayerPrefsManager.ActivatePlayerPref(_hasPayedTroyCoinEvent);
        _playerCanInteract = false;
        GetComponent<SpriteRenderer>().sprite = _unhighlightedTable;
        //place down three troy coin
        Instantiate(_tableTroyCoin, new Vector3(3.6f, -1.5f, 2), Quaternion.identity);
        OnPlayerPaysTroyCoin?.Invoke();
        //remove three troy coin from inventory
        print("inventory before removing troy coins from table: " + PlayerPrefs.GetString("p_inventory"));
        PlayerPrefsManager.RemoveAllOfObjectTypeFromInventory("troycoin");
        print("inventory after removing troy coins from table: " + PlayerPrefs.GetString("p_inventory"));
        //find the scenelocker script
        SceneLocker sceneLocker = GameObject.Find("Scene Manager").GetComponent<SceneLocker>();
        sceneLocker.IsLocked = false;

        ResetToDefaults();

    }

    public void ResetToDefaults()
    {
        GetComponent<SpriteRenderer>().sprite = _unhighlightedTable;
    }
}
