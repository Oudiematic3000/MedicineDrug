
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;


public class OperationQueueUI : MonoBehaviour
{
    ObjectPool<OperationQueueUI> pool;
    public Queue<OperationBubble> operationBubbles=new Queue<OperationBubble>();
    public List<OperationBubble> operationBubblesList = new List<OperationBubble>();
    public Interactable owner;
    void Start()
    {
        foreach (Transform t in transform)
        {
            t.GetComponent<OperationBubble>().queueUI = this;
            operationBubblesList.Add(t.GetComponent<OperationBubble>());
            operationBubbles.Enqueue(t.GetComponent<OperationBubble>());


        }
        GenerateQueue();
        operationBubbles.Peek().Run();
    }
    public void SetPool(ObjectPool<OperationQueueUI> pool)
    {
        this.pool = pool;
    }
    public void Init(Interactable owner)
    {
        this.owner = owner;
    }
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(owner.transform.position + (new Vector3(1,1,0) * 1.5f));
    }

    public void DequeueOperation()
    {
        if(operationBubbles.Count <= 0)return;

        var topBubble = operationBubbles.Peek();
        operationBubbles.Dequeue();
        Destroy(topBubble.gameObject);
        operationBubbles.Peek().Run();
        owner.template.toolNeeded=GetToolNeeded();

    }
    public void GenerateQueue()
    {
        UsableTemplate[] list = new UsableTemplate[3];
        list = Resources.LoadAll<UsableTemplate>("Tools");

        for (int i = 0; i < 3; i++)
        {
            operationBubblesList[i].toolNeeded = list[Random.Range(0, list.Length)];
        }
        owner.template.toolNeeded = GetToolNeeded();
    }
    public UsableTemplate GetToolNeeded()
    {
        return operationBubbles.Peek().toolNeeded;
    }
}
