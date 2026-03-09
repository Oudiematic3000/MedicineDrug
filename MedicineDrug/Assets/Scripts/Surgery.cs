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
    public ParticleSystem bloodEffect, smallBloodEffect;
    public Canvas canvas;
    public AudioClip heartMonitor, suffering;
    public GameObject heart, lung, intestine;
    public float spawnForce=10f;
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
        canvas.gameObject.SetActive(true);
        allOperations = true;
    }
    public void StartQueue()
    {
        if (operationQueue != null) return;
        operationQueue = OperationQueueManager.instance.GetBar();
        operationQueue.Init(this);
        PlayHeartMonitorBeep();
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
    AudioSource src = null;
    public void PlayHeartMonitorBeep()
    {
        if (allOperations) {
            src.Stop();
            src = null; 
            return; 
        }
        float space = 1.2f;
        if (AneMachine.instance.depleted)
        { 
            space = 0.5f;
            if(!src)
            src = AudioManager.instance.PlayLoopingWhile(suffering, ()=>AneMachine.instance.depleted, 0.5f);
        }
        else
        {
            if(src)
            src.Stop();
            src = null;
        }
            AudioManager.instance.PlaySFX(heartMonitor, 0.4f);
        LeanTween.delayedCall(space, PlayHeartMonitorBeep);
    }
    public override void OnComplete()
    {
        operationQueue.DequeueOperation();
        lastOperator.IncrementOperation();
        if(lastOperator.isDirty) bloodEffect.Play();
        burstOrgans();
    }

    private void burstOrgans()
    {
        GameObject[] organs = new GameObject[3];
        organs[0] = heart;
        organs[1] = lung;
        organs[2] = intestine;
        
        Vector3 spawnDirection = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))*spawnForce;
        GameObject newOrgan = organs[Random.Range(0, 3)];
        var newSpawn = Instantiate(newOrgan, transform.position + (Vector3.up*1.2f), Quaternion.identity);
        newSpawn.GetComponent<Rigidbody>().AddForce(spawnDirection, ForceMode.Impulse);
        smallBloodEffect.Play();
    }
   
}

