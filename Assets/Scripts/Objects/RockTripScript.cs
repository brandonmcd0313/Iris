
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTripScript : MonoBehaviour
{
    public bool _playerCanTrip;
    [SerializeField] GameObject _candyPrefab;
    string _playerTrippedEventName = "p_playerHasTripped";
    [SerializeField] Vector2 _randomXRange = new Vector2(-2f, 2f);
    [SerializeField] Vector2 _randomYRange = new Vector2(-2f, 2f);
    public AudioClip UHOHNOISE;

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
            PlayerPrefsManager.ActivatePlayerPref(_playerTrippedEventName);
            //get number of candy
            string[] inventory = PlayerPrefsManager.GetItemsInInventory();
            int candyCount = 0;
            foreach (string item in inventory)
            {
                if (item == "candy")
                {
                    candyCount++;
                }
            }
            DropCandy(candyCount);
            //remove the candy from inventory
            PlayerPrefsManager.RemoveAllOfObjectTypeFromInventory("candy");
            //put all the candy on the ground a objs
            foreach (GameObject person in GameObject.FindGameObjectsWithTag("Crowd"))
            {
                print(person.gameObject.name);
                person.GetComponent<MoveOnPlayerInteraction>().OnPlayerInteraction();
            }

        }
        else if (_otherObject.tag == "Player" && _playerCanTrip == false)
        {
            print("Player's candy is safe :)   for now...");
        }
    }

    void DropCandy(int candyAmount)
    {
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }
        GetComponent<AudioSource>().PlayOneShot(UHOHNOISE);
        //spawn candy at player pos with random offset
        for (int i = 0; i < candyAmount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(_randomXRange.x, _randomXRange.y), Random.Range(_randomYRange.x, _randomYRange.y), 0);
            Instantiate(_candyPrefab, GameObject.FindGameObjectWithTag("Player").transform.position + randomOffset, Quaternion.identity);
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
