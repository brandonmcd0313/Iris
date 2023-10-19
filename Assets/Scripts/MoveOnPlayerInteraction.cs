using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

public class MoveOnPlayerInteraction : MonoBehaviour
{

    Animator _animator;

    [TooltipAttribute("The object to move to when spooked.")]
    [SerializeField] GameObject _targetObject;
    [SerializeField] string _targetObjectName;
    [SerializeField] float _speed = 3f;
    
    [SerializeField] string _animationName;

    bool _isUsingAnimator = false;
    [MinMaxSlider(-10f, 10f)]
    [SerializeField] Vector2 _randomXRange = new Vector2(2f, 8f);
    [MinMaxSlider(-10f, 10f)]
    [SerializeField] Vector2 _randomYRange = new Vector2(2f, 8f);
    // Start is called before the first frame update
    void Start()
    {
        if (_targetObject == null)
        {
            _targetObject = GameObject.Find(_targetObjectName);
        }
        if (GetComponent<Animator>() != null)
        { _animator = GetComponent<Animator>();
            _isUsingAnimator = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayerInteraction()
    {
        ;
        if (_isUsingAnimator)
        {
            _animator.SetBool(_animationName, true);
            print(_animationName);
        }
        StartCoroutine(MoveToTarget());
    }



    IEnumerator MoveToTarget()
    {
        Vector3 targetPosition = _targetObject.transform.position;
        
            targetPosition.x += Random.Range(_randomXRange.x, _randomXRange.y);
        
            targetPosition.y += Random.Range(_randomYRange.x, _randomYRange.y);
        
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(
            transform.position, targetPosition,
                _speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return new WaitForSeconds(1.5f);
        if (_isUsingAnimator)
        {
            _animator.SetBool(_animationName, false);
        }
    }
}


