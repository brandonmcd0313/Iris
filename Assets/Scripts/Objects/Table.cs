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
        if (_playerCanInteract)
        {
            GetComponent<SpriteRenderer>().sprite = _highlightedTable;
        }
    }

    public void OnPlayerInteract()
    {
        //place down three troy coin
        OnPlayerPaysTroyCoin?.Invoke();
        //remove three troy coin from inventory
        PlayerPrefsManager.RemoveAllTroyCoinsFromInventory();

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
