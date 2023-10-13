using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour, IInteractable
{
    [SerializeField] Color _highlightColor;
    [SerializeField] GameObject[] _birds;
    bool _hasScaredBirds;
    Color _defaultColor;
    Vector3 _defaultScale;

   public Action OnPlayerGrabBag;

    void Start()
    {
        _defaultColor = GetComponent<SpriteRenderer>().color;
        _defaultScale = transform.localScale;
        
    }

    public void OnPlayerApproach()
    {
        //TODO: change to a sprite swap
        GetComponent<SpriteRenderer>().color = _highlightColor;
        //make object slightly bigger
        transform.localScale = _defaultScale * 1.1f;
    }

    public void OnPlayerInteract()
    {
        OnPlayerGrabBag?.Invoke();
        //TODO: make this do something meaningful
        Debug.Log("Player interacted with " + gameObject.name);
        //scare all of the birds
        if (_hasScaredBirds)
        {
            return;
        }
        foreach (GameObject bird in _birds)
        {
            bird.GetComponent<MoveOnPlayerInteraction>().OnPlayerInteraction();
        }

        _hasScaredBirds = true;

    }

    public void ResetToDefaults()
    {
        GetComponent<SpriteRenderer>().color = _defaultColor;
        transform.localScale = _defaultScale;
    }
}
