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
    [SerializeField] private StackBrain stackBrain;

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
        stackBrain = GetComponentInChildren<StackBrain>();
    }

    private void Update()
    {
        /**
         * Ground Check
         */
        RaycastHit _hit;
        bool _isHit = Physics.Raycast(transform.position, Vector3.down, out _hit, downRaycastDistance);

        if (_isHit && (_hit.transform.CompareTag("Ground") || _hit.transform.CompareTag("Stack")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (!isGrounded && isHaveStack)
        {
            //Lost Stack
            Stack lastStack = stackBrain.collectedList[stackBrain.collectedList.Count - 1];
            lastStack.transform.parent = null;
            Destroy(lastStack.stackJoint);
            lastStack.stackRigidbody.isKinematic = true;
            lastStack.transform.position = transform.position + ((Vector3.forward * 0.11f) + (Vector3.down * 0.1f));
            lastStack.transform.LookAt(Vector3.forward);

            stackBrain.collectedList.Remove(lastStack);
            stackBrain.OnLostStack();
        }

        if (!isGrounded && !isHaveStack)
        {
            playerAnimator.SetBool("isFalling", true);
        }
        
    }

}