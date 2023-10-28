using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBowl : MonoBehaviour, IInteractable
{
    [SerializeField] Sprite _defaultBowl;
    [SerializeField] Sprite _highlightedBowl;
    [SerializeField] AudioClip _onCandyPickup;
    public void OnPlayerApproach()
    {
        //highlight the bowl
        GetComponent<SpriteRenderer>().sprite = _highlightedBowl;
        
    }

    public void OnPlayerInteract()
    {
        print("on interact tried added candy");
        //check if the player has a full inventory
        if (PlayerPrefsManager.GetItemsInInventory().Length >= 9)
        {

            //if they do, do nothing
            return;
        
         }
        else
        {
            //add a candy to inventory
            //if they don't, add a candy to the inventory
            GetComponent<AudioSource>().PlayOneShot(_onCandyPickup);
            PlayerPrefsManager.AddObjectToInventory("pItem_candy");
        }
    }

    public void ResetToDefaults()
    {
        //reset the bowl
        GetComponent<SpriteRenderer>().sprite = _defaultBowl;
    }

    
}
