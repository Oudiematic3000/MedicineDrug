using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class ProgressBarManager : MonoBehaviour
{
    public ProgressBar barPrefab;
    public static ProgressBarManager instance;
    ObjectPool<ProgressBar> pool;

    private void Awake()
    {
     
        instance = this;
        pool = new ObjectPool<ProgressBar>(
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
    ProgressBar CreateBar()
    {
        var go = Instantiate(barPrefab, this.transform);
        var bar=go.GetComponent<ProgressBar>();
        bar.SetPool(pool);
        return bar;

    }

    void OnTakeBar(ProgressBar bar)
    {
        bar.gameObject.SetActive(true);
    }
    void OnReturnBar(ProgressBar bar)
    {
        bar.gameObject.SetActive(false);
    }
    void OnDestroyBar(ProgressBar bar)
    {
        Destroy(bar.gameObject);
    }
    public ProgressBar GetBar()
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
