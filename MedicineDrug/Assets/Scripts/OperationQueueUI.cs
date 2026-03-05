
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;


public class OperationQueueUI : MonoBehaviour
{
    ObjectPool<OperationQueueUI> pool;
    public Queue<OperationBubble> operationBubbles=new Queue<OperationBubble>();
    public Interactable owner;
    void Start()
    {
        foreach(Transform t in transform)
        {
            t.GetComponent<OperationBubble>().queueUI = this;
            operationBubbles.Enqueue(t.GetComponent<OperationBubble>());
        }
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
        print("OPBUBS"+operationBubbles.Count);
        operationBubbles.Peek().Run();

    }
}
