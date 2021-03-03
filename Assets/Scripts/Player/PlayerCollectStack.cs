using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectStack : MonoBehaviour
{

    [Header("References")] 
    [SerializeField] private StackBrain _stackBrain;
    [SerializeField] private Material collectedMaterial;

    private Stack _lastAdded;

    private void Start()
    {
        _stackBrain.GetComponentInChildren<StackBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Stack _stack = other.gameObject.AddComponent<Stack>();

            Renderer _renderer = other.gameObject.GetComponent<Renderer>();
            _renderer.material = collectedMaterial;
            
            _stackBrain.collectedList.Add(_stack);
            _stack.stackID = _stackBrain.collectedList.IndexOf(_stack);

            other.gameObject.name = "Stack " + _stack.stackID;
            other.gameObject.tag = "Stack";

            other.transform.parent = _stackBrain.gameObject.transform;
            other.transform.localPosition = (Vector3.up * _stack.stackID * 0.12f);
            FixedJoint _joint = other.gameObject.AddComponent<FixedJoint>();

            _stack.stackCollider.isTrigger = false;
            _stack.stackRigidbody.isKinematic = false;

            if (_lastAdded)
            {
                _joint.connectedBody = _lastAdded.stackRigidbody;
            }
            else
            {
                _stack.stackRigidbody.isKinematic = true;
            }

            _lastAdded = _stack;
            _stackBrain.OnCollectStack();

        }
    }
}
