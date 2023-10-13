using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public Action OnPlayerExitScreenSpaceRight;
    public Action OnPlayerExitScreenSpaceLeft;
    [SerializeField] float _speed = 5f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //set rigidbody to kinematic
        GetComponent<Rigidbody2D>().isKinematic = true;
        animator = GetComponent<Animator>();
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
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("IsWalking", true);
        }


    }
    
}
