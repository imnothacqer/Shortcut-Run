using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Booleans")] 
    [SerializeField] private bool isCanMove;
    [SerializeField] private bool isCanSlide;

    [Header("Values")] 
    [SerializeField] private float movementSpeed;
    [SerializeField] private float slideSensivity;

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
}
