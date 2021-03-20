using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Booleans")] 
    [SerializeField] public bool isCanMove;
    [SerializeField] public bool isCanSlide;

    [Header("Values")] 
    [SerializeField] public float movementSpeed;
    [SerializeField] public float slideSensivity;

    public Transform PlayerBody;

    private float inputHorizontal;
    

    private void Update()
    {
        if (isCanSlide)
        {
            inputHorizontal = SimpleInput.GetAxis("Horizontal");
            Vector3 _direction = new Vector3();
            _direction.y = slideSensivity * inputHorizontal;
            
            transform.Rotate(_direction * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (isCanMove)
        {
            transform.Translate(transform.forward * movementSpeed * Time.fixedDeltaTime);
        }
    }
}
