using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour, IInteractable
{
    [SerializeField] Sprite _orange;
    [SerializeField] Sprite _defaultSprite;
    [SerializeField] GameObject[] _birds;
    bool _hasScaredBirds;
    Vector3 _defaultScale = new Vector3(0.77f, 0.77f, 0.77f);

   public Action OnPlayerGrabBag;

    void Start()
    {
        _defaultSprite = GetComponent<SpriteRenderer>().sprite;
        _defaultScale = transform.localScale;
        //set birds to any object with the tag "Bird"
        _birds = GameObject.FindGameObjectsWithTag("Bird");
    }

    public void OnPlayerApproach()
    {
        //TODO: change to a sprite swap
        GetComponent<SpriteRenderer>().sprite = _orange;
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
        Destroy(gameObject);    
    }

    public void ResetToDefaults()
    {
        GetComponent<SpriteRenderer>().sprite = _defaultSprite;
        transform.localScale = _defaultScale;
    }
}
