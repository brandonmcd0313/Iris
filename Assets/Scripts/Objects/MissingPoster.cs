using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingPoster : MonoBehaviour, IInteractable
{
    [SerializeField] Color _defaultColor;
    [SerializeField] Color _highlightColor;
    [SerializeField] GameObject _posterPrefab;
    SpriteRenderer _sr;
    
    void Start()
    {
        //set the default color
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = _defaultColor;
    }
    
    public void OnPlayerApproach()
    {
        _sr.color = _highlightColor;
    }

    public void OnPlayerInteract()
    {
        Instantiate(_posterPrefab, transform.position, Quaternion.identity);
    }

    public void ResetToDefaults()
    {
        _sr.color = _defaultColor;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
