using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Surgery : Interactable
{
    public OperationQueueUI operationQueue;
    Player lastOperator;
    public bool allOperations = false;
    public GurneyBody body;
    void Start()
    {
        
    }
    public void AllOperationsComplete()
    {
        BoxCollider[] colliders = body.GetComponentsInChildren<BoxCollider>();

        int exitWallLayer = LayerMask.NameToLayer("ExitWall");

        foreach (var col in colliders)
        {
            col.excludeLayers = 1 << exitWallLayer;
        }
        body.GetComponent<BoxCollider>().excludeLayers = 1 << exitWallLayer;
    }
    public void StartQueue()
    {
        if (operationQueue != null) return;
        operationQueue = OperationQueueManager.instance.GetBar();
        operationQueue.Init(this);
    }
    public override void OnInteract(bool action, Player player)
    {
        if(operationQueue == null) return;
        if (operationQueue.operationBubbles.Count < 1) return;
        if (player.isDirty) return;
        if (!AneMachine.instance.depleted)
        {
            lastOperator = player;
            base.OnInteract(action, player);
            
        }
    }
    public override void OnComplete()
    {
        operationQueue.DequeueOperation();
        lastOperator.IncrementOperation();
    }
   
}

