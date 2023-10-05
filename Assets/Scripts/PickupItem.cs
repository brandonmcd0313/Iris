using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] Color _highlightColor;
    bool _isHighlighted = false;
    Color _defaultColor;
    Vector3 _defaultScale;
    
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
        _isHighlighted = true;
    }
    
    public void OnPlayerInteract()
    {
        //TODO: make this do something meaningful
        Debug.Log("Player interacted with " + gameObject.name);
        Destroy(gameObject);
    }

    public void ResetToDefaults()
    {
        GetComponent<SpriteRenderer>().color = _defaultColor;
        transform.localScale = _defaultScale;
        _isHighlighted = false;
    }
}

