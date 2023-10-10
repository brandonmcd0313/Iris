using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BirdControl : MonoBehaviour
{
    Animator _animator;
    public GameObject _flyToTarget;
    public float _flyToSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D _otherObject)
    {
        if (_otherObject.tag == "Player")
        {
            Debug.Log("Spooked by player.");
            _animator.SetBool("Spooked", true);
            StartCoroutine(MoveToPlayer());
        }
    }

    /*
    void OnTriggerExit2D(Collider2D _otherObject)
    {
        if (_otherObject.tag == "Player")
        {
            Debug.Log("Player is gone");
            _animator.SetBool("Spooked", false);
        }
    }
    */

    //Enumerator to Fly Away when the player moves
    IEnumerator MoveToPlayer()
    {
        while (true)
        {
            while (transform.position != _flyToTarget.transform.position)
            {
                transform.position = Vector3.MoveTowards(
                transform.position, _flyToTarget.transform.position,
                    _flyToSpeed * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            yield return new WaitForSeconds(1.5f);
        }
    }

}
