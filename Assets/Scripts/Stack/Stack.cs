using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [Header("Values")] 
    public int stackID;

    public Rigidbody stackRigidbody;
    public BoxCollider stackCollider;
    public FixedJoint stackJoint;

    private void OnEnable()
    {
        stackRigidbody = GetComponent<Rigidbody>();
        stackCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        stackJoint = GetComponent<FixedJoint>();
    }
}
