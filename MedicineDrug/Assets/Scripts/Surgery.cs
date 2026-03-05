using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Surgery : Interactable
{
   
    public Queue<UsableTemplate> operations=new Queue<UsableTemplate>();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnComplete()
    {
        operations.Dequeue();
    }
}

