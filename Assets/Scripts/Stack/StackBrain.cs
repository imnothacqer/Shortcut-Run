using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBrain : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private PlayerBrain playerBrain;

    [Header("Values")] 
    [SerializeField] public List<Stack> collectedList;
    [SerializeField] private bool isHaveStack;
    
    public Action OnCollectStack;
    public Action OnLostStack;

    private void Start()
    {
        playerBrain = GetComponentInParent<PlayerBrain>();

        OnCollectStack += CheckHaveStack;

        OnLostStack += CheckHaveStack;
    }

    private void CheckHaveStack()
    {
        if (collectedList.Count >= 1)
        {
            isHaveStack = true;
        }
        else
        {
            isHaveStack = false;
        }
        playerBrain.IsHaveStack = isHaveStack;
        
    }
}
