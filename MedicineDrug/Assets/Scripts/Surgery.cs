using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Surgery : Interactable
{
    public OperationQueueUI operationQueue;
    
    
    void Start()
    {
        
    }

    public void StartQueue()
    {
        if (operationQueue != null) return;
        operationQueue = OperationQueueManager.instance.GetBar();
        operationQueue.Init(this);
    }
    public override void OnInteract(bool action, Player player)
    {
        if (operationQueue.operationBubbles.Count < 1) return;
        if (!AneMachine.instance.depleted)
        {
            print("SurgeryInteract toolNeeded: " + template.toolNeeded + " ToolHeld: " + player.heldTool.template);
            base.OnInteract(action, player);
        }
    }
    public override void OnComplete()
    {
        operationQueue.DequeueOperation();
    }
   
}

