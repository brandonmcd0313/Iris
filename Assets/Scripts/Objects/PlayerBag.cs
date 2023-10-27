using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour, IInteractable
{
    [SerializeField] Sprite _orange;
    [SerializeField] Sprite _defaultSprite;
    Vector3 _defaultScale = new Vector3(0.77f, 0.77f, 0.77f);

   public Action OnPlayerGrabBag;

    [SerializeField]  AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _defaultSprite = GetComponent<SpriteRenderer>().sprite;
        _defaultScale = transform.localScale;
        //set birds to any object with the tag "Bird"
    
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

        Debug.Log("Picked up");
        this.GetComponent<AudioSource>().Play();

        foreach (GameObject bird in GameObject.FindGameObjectsWithTag("Bird"))
        {
            bird.GetComponent<MoveOnPlayerInteraction>().OnPlayerInteraction();
            bird.GetComponent<AudioSource>().Play();
        }


        Destroy(gameObject);    
    }

    public void ResetToDefaults()
    {
        GetComponent<SpriteRenderer>().sprite = _defaultSprite;
        transform.localScale = _defaultScale;
    }
}
