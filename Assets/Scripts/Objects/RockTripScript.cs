using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTripScript : MonoBehaviour
{
    bool _playerCanTrip;
   
    
    // Start is called before the first frame update
    void Start()
    {
       InvokeRepeating("CheckIfPlayerCanTrip",0, 0.5f);
    }


    void OnTriggerEnter2D(Collider2D _otherObject)
    {
        if (_otherObject.tag == "Player" && _playerCanTrip == true)
        {

        }
    }


    void CheckIfPlayerCanTrip()
    {
        //player can interact if they have three troycoin
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
            _playerCanTrip = true;
        }
        else
        {
            _playerCanTrip = false;
        }
    }

}
