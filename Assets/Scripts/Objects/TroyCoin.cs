using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TroyCoin : MonoBehaviour, IInteractable
{
    [SerializeField] Sprite _dullCoin;
    [SerializeField] Sprite _shinyCoin;
   
     AudioSource _audioSource;
    [SerializeField] AudioClip _TroyCoinSound;
    [SerializeField] AudioClip _failedPickup;
    string _bagEvent = "p_hasPlayerBagBeenGrabed";
    bool _willScareBirds = false;

    TroyCoinSpawnManager _instance;

    Animator _anim;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //assign the instance of TroyCoinSpawnManager
        _instance = FindObjectOfType<TroyCoinSpawnManager>();

        _anim = GetComponent<Animator>();
        ResetToDefaults();

        //if birds exist in the scene, set _willScareBirds to true
        if (GameObject.FindGameObjectsWithTag("Bird").Length > 0)
        {
            _willScareBirds = true;
        }
    }
    
    public void OnPlayerApproach()
    {
        //make coin shiny and spin
        GetComponent<SpriteRenderer>().sprite = _shinyCoin;
        _anim.Play("TroyCoin-Spin");
        _anim.enabled = true;
    }

    public void OnPlayerInteract()
    {
        Debug.Log("Picked up");
        if(!PlayerPrefsManager.HasPlayerPrefBeenActivated(_bagEvent))
        {
            //if the player has not picked up the bag, play the failed pickup sound
            _audioSource.PlayOneShot(_failedPickup);
            return;
        }
        _instance.OnTroyCoinGrab();
        _audioSource.PlayOneShot(_TroyCoinSound);
        //if birds exist in the scene, scare them
        if (_willScareBirds)
        {
            foreach (GameObject bird in GameObject.FindGameObjectsWithTag("Bird"))
            {
                bird.GetComponent<MoveOnPlayerInteraction>().OnPlayerInteraction();
            }
        }
        GetComponent<SpriteRenderer>().color = new Color (0,0,0,0);
        Invoke("DestroyObject", 0.5f);
       
    }

    void DestroyObject()
    {
        //TODO: add to inventory
        Destroy(gameObject);
    }
    public void ResetToDefaults()
    {
        try
        {
            //make coin dull and stop spinning
            GetComponent<SpriteRenderer>().sprite = _dullCoin;
            _anim.StopPlayback();
            _anim.enabled = false;
        }
        catch (NullReferenceException)
        {
            //do nothing 
        }
    }

    
}
