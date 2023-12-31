using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPlayerInteraction : MonoBehaviour
{

    Animator _animator;

    [TooltipAttribute("The object to move to when spooked.")]
    [SerializeField] GameObject _targetObject;
    [SerializeField] GameObject _targetObjectTwo;
    [SerializeField] string _targetObjectName;
    [SerializeField] float _speed = 3f;
    [SerializeField] bool _moveOnYAxis = true;
    [SerializeField] string _animationName;

    bool _isUsingAnimator = false;
    [SerializeField] Vector2 _randomXRange = new Vector2(2f, 8f);
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
    
        

        //if second target is not null 50/50 change to choose it instead
        if (_targetObjectTwo != null && Random.Range(0, 2) == 1)
        {
            targetPosition = _targetObjectTwo.transform.position;

            targetPosition.x += Random.Range(_randomXRange.x, _randomXRange.y);

            targetPosition.y += Random.Range(_randomYRange.x, _randomYRange.y);
        }

        if (!_moveOnYAxis)
        {
            targetPosition.y = transform.position.y;
        }
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

        Destroy(gameObject);
    }
}


