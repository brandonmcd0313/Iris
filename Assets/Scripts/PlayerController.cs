using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //starts as true for the first scene :)
    public static bool IsEnteringSceneFromTheLeft = true;
    public Action OnPlayerExitScreenSpaceRight;
    public Action OnPlayerExitScreenSpaceLeft;
    float _minXPosition;
    float _maxXPosition;
    private float _spawnOffset = 1f;
    private float _yPosition = -3.55f;
    [SerializeField] float _speed = 5f;
    public Animator animator;
    //AudioSource _audioSource;
    //SerializeField] AudioClip _muddyWalkSound;

    // Start is called before the first frame update
    void Start()
    {
        //set rigidbody to kinematic
        GetComponent<Rigidbody2D>().isKinematic = true;
        animator = GetComponent<Animator>();
        //_audioSource = GetComponent<AudioSource>();

        //set the max and min x positions to the camera bounds
        _minXPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        _maxXPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;

        //move the player object to its starting position
        if (IsEnteringSceneFromTheLeft)
        {
            transform.position = new Vector2(_minXPosition + _spawnOffset, _yPosition);
            
        }
        else
        {
            transform.position = new Vector2(_maxXPosition - _spawnOffset, _yPosition);
        }

        OnPlayerExitScreenSpaceRight += OnExitSceneFromTheRight;
        OnPlayerExitScreenSpaceLeft += OnExitSceneFromTheLeft;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsWalking", false); 

        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * _speed * Time.deltaTime);
        if(horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("IsWalking", true);
            //_audioSource.PlayOneShot(_muddyWalkSound);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("IsWalking", true);
            //_audioSource.PlayOneShot(_muddyWalkSound);
        }

        //if on the right side of the screen and moving right
        if (transform.position.x > _maxXPosition && horizontalInput > 0)
        {
            //call the event
            OnPlayerExitScreenSpaceRight?.Invoke();
        }

        //if on the left side of the screen and moving left
        if (transform.position.x < _minXPosition && horizontalInput < 0)
        {
            //call the event
            OnPlayerExitScreenSpaceLeft?.Invoke();
        }

        //clamp the player position to the camera bounds
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, _minXPosition, _maxXPosition), transform.position.y);

    }

    void OnExitSceneFromTheRight()
    {
        IsEnteringSceneFromTheLeft = true;
    }

    void OnExitSceneFromTheLeft()
    {
        IsEnteringSceneFromTheLeft = false;
    }

}
