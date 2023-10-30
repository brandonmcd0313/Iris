using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkOrTreatManager : MonoBehaviour
{
    string _playerTrippedEventName = "p_playerHasTripped";
    string _carsHaveLeft = "p_carsHaveLeft";
    [SerializeField] AudioClip _carLeavingSound;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefsManager.HasPlayerPrefBeenActivated(_playerTrippedEventName))
        {
            //do nothing
            return;
        }
        if (!PlayerPrefsManager.HasPlayerPrefBeenActivated(_carsHaveLeft))
        {
            //play the sound
           AudioSource.PlayClipAtPoint(_carLeavingSound, Camera.main.transform.position);
            //set the player pref
            PlayerPrefsManager.ActivatePlayerPref(_carsHaveLeft);
        }

        //kill all the cars
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Car"))
        {
            Destroy(obj);
        }
        //kill all the kids
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Crowd"))
        {
            Destroy(obj);
        }
        //kill all the candy
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Candy"))
        {
            Destroy(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
