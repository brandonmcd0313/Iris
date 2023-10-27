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
        _playerCanInteract = false;
        GetComponent<SpriteRenderer>().sprite = _unhighlightedTable;
       //place down three troy coin
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
