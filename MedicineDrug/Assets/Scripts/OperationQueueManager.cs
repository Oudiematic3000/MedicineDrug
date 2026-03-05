using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class OperationQueueManager : MonoBehaviour
{
    public OperationQueueUI queuePrefab;
    public static OperationQueueManager instance;
    ObjectPool<OperationQueueUI> pool;

    private void Awake()
    {
     
        instance = this;
        pool = new ObjectPool<OperationQueueUI>(
            createFunc: CreateBar,
            actionOnGet: OnTakeBar,
            actionOnRelease: OnReturnBar,
            actionOnDestroy: OnDestroyBar,
            collectionCheck: true,
            defaultCapacity: 100,
            maxSize: 200
            );

    }
  
    void Start()
    {

    }
    OperationQueueUI CreateBar()
    {
        var go = Instantiate(queuePrefab, this.transform);
        var bar=go.GetComponent<OperationQueueUI>();
        bar.SetPool(pool);
        return bar;

    }

    void OnTakeBar(OperationQueueUI bar)
    {
        bar.gameObject.SetActive(true);
    }
    void OnReturnBar(OperationQueueUI bar)
    {
        bar.gameObject.SetActive(false);
    }
    void OnDestroyBar(OperationQueueUI bar)
    {
        Destroy(bar.gameObject);
    }
    public OperationQueueUI GetBar()
    {
        return pool.Get();
    }
    public void ClearPool()
    {
        pool.Clear();
    }
    void Update()
    {

    }
}
