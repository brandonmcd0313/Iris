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
    // Start is called before the first frame update
    void Start()
    {
        //set rigidbody to kinematic
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * _speed * Time.deltaTime);
        if(horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
        CheckPositionOnScreen();
    }

    void CheckPositionOnScreen()
    {
        //translate player position to screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        //if the player is off the screen, call the next scene
        if (screenPos.x > Screen.width)
        {
            OnPlayerExitScreenSpaceRight?.Invoke();
        }
        else if (screenPos.x < 0)
        {
            OnPlayerExitScreenSpaceLeft?.Invoke();
        }
    }
}
