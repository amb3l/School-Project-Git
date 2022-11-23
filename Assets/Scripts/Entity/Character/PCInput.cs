using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MovementController))]
public class PCInput : MonoBehaviour
{
    MovementController playerMovement;
    float move;
    bool isCrawling;
    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<MovementController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            move = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
            }

            isCrawling = Input.GetKey(KeyCode.C);

            if (Input.GetButtonDown("Throw"))
            {
                playerMovement.StartThrowing();
            }

            if (Input.GetButtonDown("Kick"))
            {
                playerMovement.StartKicking();
            }
        }
    }

    void FixedUpdate()
    {
        playerMovement.Move(move, isCrawling, isJumping);
        isJumping = false;
    }
}
