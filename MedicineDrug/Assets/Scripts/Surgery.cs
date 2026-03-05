using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Surgery : Interactable
{
    public OperationQueueUI operationQueue;
    
    void Start()
    {
        operationQueue = OperationQueueManager.instance.GetBar();
        operationQueue.Init(this);
    }

 
    public override void OnInteract(bool action, Player player)
    {
        base.OnInteract(action, player);
    }
    public override void OnComplete()
    {
        operationQueue.DequeueOperation();
    }
}

