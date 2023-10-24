using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    //make this a singleton
    public static PlayerPrefsManager instance;
    static string playerPrefInventoryName = "p_inventory";
    /* PREFIX
     *  all player prefs start with p_
     */

    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        if (instance == null)
        {
            instance = this;
            //create an inventory for the player
           
            if (!PlayerPrefs.HasKey(playerPrefInventoryName))
            {
                PlayerPrefs.SetString(playerPrefInventoryName, "");
            }
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
        if (playerPrefName.Contains("pItem"))
        {
            AddObjectToInventory(playerPrefName);
        }
    }

    public static void DeactivatePlayerPref(string playerPrefName)
    {
        if (!PlayerPrefs.HasKey(playerPrefName))
        {
            PlayerPrefs.SetInt(playerPrefName, 0);
        }

        PlayerPrefs.SetInt(playerPrefName, 0);
        if (playerPrefName.Contains("pItem"))
        {
            RemoveObjectFromInventory(playerPrefName);
        }
    }
    static void AddObjectToInventory(string playerPrefName)
    {
        //inventory is all objects tagged with "pItem"
        //the inventory player pref has dashes "-" between the names of the objects

        //check if the item is already in the inventory
     
        if (PlayerPrefs.GetString(playerPrefInventoryName).Contains(playerPrefName))
        {
            return;
        }
        PlayerPrefs.SetString(playerPrefInventoryName, PlayerPrefs.GetString(playerPrefInventoryName) + "-" + playerPrefName);
    }

    static void RemoveObjectFromInventory(string playerPrefName)
    {
        //inventory is all objects tagged with "pItem"
        //the inventory player pref has dashes "-" between the names of the objects

        //check if the item is already in the inventory
        if (!PlayerPrefs.GetString(playerPrefInventoryName).Contains(playerPrefName))
        {
            //if not in inventory return
            return;
        }
        string stringToRemove = "-" + playerPrefName; //remove the dash
        PlayerPrefs.SetString(playerPrefInventoryName, PlayerPrefs.GetString(playerPrefInventoryName).Replace(stringToRemove, ""));
    }

    public static void RemoveAllTroyCoinsFromInventory()
    {
        //split the inventory into parts
        string[] inventoryItemPlayerPrefs = PlayerPrefs.GetString(playerPrefInventoryName).Split('-');
        foreach (string playerPrefName in inventoryItemPlayerPrefs)
        {
            if (playerPrefName.Contains("troycoin"))
            {
                RemoveObjectFromInventory(playerPrefName);
            }
        }
    }
    
    static string[] GetItemsInInventory()
    {
        //will return a list of only the TYPE of the item in the inventory
        //can be bag, troycoin, or candy
        string[] inventoryItemPlayerPrefs = PlayerPrefs.GetString(playerPrefInventoryName).Split('-');
        //arraylist for items
        List<string> items = new List<string>();
        foreach (string item in inventoryItemPlayerPrefs)
        {
            
           if(item.ToLower().Contains("bag"))
            {
                items.Add("bag");
            }
            else if (item.ToLower().Contains("troycoin"))
            {
                items.Add("troycoin");
            }
            else if (item.ToLower().Contains("candy"))
            {
                items.Add("candy");
            }
        }

        //convert list to array
        string[] itemsArray = new string[items.Count];
        for (int i = 0; i < items.Count; i++)
        {
            itemsArray[i] = items[i];
        }
        return itemsArray;
    }
}
