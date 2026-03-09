
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;


public class OperationQueueUI : MonoBehaviour
{
    ObjectPool<OperationQueueUI> pool;
    public Queue<OperationBubble> operationBubbles=new Queue<OperationBubble>();
    public List<OperationBubble> operationBubblesList = new List<OperationBubble>();
    public Vector3 placement = new Vector3(0.3f, 6f, 0);
    public float scalar=1f;
    public Surgery owner;
    void Start()
    {
        foreach (Transform t in transform)
        {
            var bubble = t.GetComponent<OperationBubble>();
            bubble.queueUI = this;
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
    public void Init(Surgery owner)
    {
        this.owner = owner;
    }
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(owner.transform.position + (placement * scalar));
    }

    public void DequeueOperation()
    {
        if(operationBubbles.Count <= 0)return;

        var topBubble = operationBubbles.Peek();
        operationBubbles.Dequeue();
        Destroy(topBubble.gameObject);
        if (operationBubbles.Count <= 0) { owner.AllOperationsComplete(); return; }
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
            operationBubblesList[i].testText.text = operationBubblesList[i].toolNeeded.name;
            operationBubblesList[i].image.sprite = operationBubblesList[i].toolNeeded.sprite;
        }
        owner.template.toolNeeded = GetToolNeeded();
    }
    public UsableTemplate GetToolNeeded()
    {
        return operationBubbles.Peek().toolNeeded;
    }
}
