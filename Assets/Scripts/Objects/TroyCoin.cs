using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TroyCoin : MonoBehaviour, IInteractable
{
    [SerializeField] Sprite _dullCoin;
    [SerializeField] Sprite _shinyCoin;
    bool _willScareBirds = false;

    TroyCoinSpawnManager _instance;

    Animator _anim;
    void Start()
    {
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
        _instance.OnTroyCoinGrab();
        //if birds exist in the scene, scare them
        if (_willScareBirds)
        {
            foreach (GameObject bird in GameObject.FindGameObjectsWithTag("Bird"))
            {
                bird.GetComponent<MoveOnPlayerInteraction>().OnPlayerInteraction();
            }
        }
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
