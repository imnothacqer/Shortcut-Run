using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Running,
    Falling,
    Climbing
}

public class PlayerBrain : MonoBehaviour
{
    public PlayerState currentPlayerState;

    [Header("References")] [SerializeField]
    private Animator playerAnimator;

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Values")] 
    [SerializeField] private float downRaycastDistance;


    [SerializeField] private bool isRunning;
    [SerializeField] private bool isHaveStack;
    [SerializeField] private bool isGrounded;


    /**
     * 
     */

    public bool IsHaveStack
    {
        get { return isHaveStack; }
        set
        {
            isHaveStack = value;
            playerRigidbody.useGravity = !value;
            playerAnimator.SetBool("isHaveStack", value);
        }
    }
    
    public bool IsRunning
    {
        get { return isRunning; }
        set
        {
            isRunning = value;
            playerMovement.isCanMove = value;
            playerMovement.isCanSlide = value;
            playerAnimator.SetBool("isRunning", value);
        }
    }

    private void Start()
    {
        // Set Components
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        /**
         * Ground Check
         */
        RaycastHit _hit;
        bool _isHit = Physics.Raycast(transform.position, Vector3.down, out _hit, downRaycastDistance);

        if (_isHit && (_hit.transform.tag == "Ground" || _hit.transform.tag == "Stack"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        /**
         * Set State
         */
        if (isGrounded && isRunning)
        {
            currentPlayerState = PlayerState.Running;
        }
        else if (!isGrounded && isRunning)
        {
            currentPlayerState = PlayerState.Falling;
        }
        else if (isRunning && isGrounded)
        {
            currentPlayerState = PlayerState.Idle;
        }

        /**
         * Set Gravity
         */
    }

}