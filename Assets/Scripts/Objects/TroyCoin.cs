using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TroyCoin : MonoBehaviour, IInteractable
{
    [SerializeField] Sprite _dullCoin;
    [SerializeField] Sprite _shinyCoin;

    Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
        ResetToDefaults();
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
        //TODO: add to inventory
        Destroy(gameObject);
    }

    public void ResetToDefaults()
    {
        //make coin dull and stop spinning
        GetComponent<SpriteRenderer>().sprite = _dullCoin;
        _anim.StopPlayback();
        _anim.enabled = false;
    }

    
}
