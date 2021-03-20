using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
    [SerializeField] private bool groundIsStack;


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
        // playerAnimator = GetComponent<Animator>();
        // playerRigidbody = GetComponent<Rigidbody>();
        // playerMovement = GetComponent<PlayerMovement>();
        // stackBrain = GetComponentInChildren<StackBrain>();
    }

    private void Update()
    {
        /**
         * Ground Check
         */
        CheckGround();


        if (!isGrounded && isHaveStack)
        {
            PlaceStack();
        }

        if (!isGrounded && !isHaveStack)
        {
            //SetFalling();
        }
    }

    private void CheckGround()
    {
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, downRaycastDistance))
        {
            if (_hit.transform.tag == "Ground")
            {
                Debug.Log("is Ground");
            }
        }
    }

    private void PlaceStack()
    {
        Stack lastStack = stackBrain.collectedList[stackBrain.collectedList.Count - 1];
        lastStack.transform.parent = null;
        Destroy(lastStack.stackJoint);
        lastStack.stackRigidbody.isKinematic = true;
        lastStack.transform.position = transform.position;

        lastStack.transform.position += new Vector3(
            lastStack.transform.localScale.x,
            0,
            lastStack.transform.localScale.z
        ) * lastStack.transform.rotation.y;

        lastStack.gameObject.isStatic = true;

        stackBrain.collectedList.Remove(lastStack);
        stackBrain.OnLostStack();
    }

    private void SetFalling()
    {
        playerAnimator.SetBool("isFalling", true);
    }
}