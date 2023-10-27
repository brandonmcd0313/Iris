using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTripScript : MonoBehaviour
{
    public bool _playerCanTrip;
   
    
    // Start is called before the first frame update
    void Start()
    {
       InvokeRepeating("CheckIfPlayerCanTrip",0, 0.5f);
    }


    void OnTriggerEnter2D(Collider2D _otherObject)
    {
        print("Player hits rock.");

        if (_otherObject.tag == "Player" && _playerCanTrip == true)
        {
            //remove the candy from inventory
            print("inventory before removing candy from bag: " + PlayerPrefs.GetString("p_inventory"));
            PlayerPrefsManager.RemoveAllOfObjectTypeFromInventory("candy");
            print("inventory after removing candy from bag: " + PlayerPrefs.GetString("p_inventory"));

            foreach (GameObject person in GameObject.FindGameObjectsWithTag("Crowd"))
            {
                person.GetComponent<MoveOnPlayerInteraction>().OnPlayerInteraction();
            }

        }
        else if (_otherObject.tag == "Player" && _playerCanTrip == false)
        {
            print("Player's candy is safe :)   for now...");
        }
    }


    void CheckIfPlayerCanTrip()
    {
        //player can trip if they have 3 or more candy
        string[] inventory = PlayerPrefsManager.GetItemsInInventory();
        int candyCount = 0;
        foreach (string item in inventory)
        {
            if (item == "candy")
            {
                candyCount++;
            }
        }

        if (candyCount >= 4)
        {
            print("Player can trip");
            _playerCanTrip = true;
        }
        else
        {
            //print("Player can't trip");
            _playerCanTrip = false;
        }
    }

}
