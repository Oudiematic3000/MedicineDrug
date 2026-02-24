using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ProgressBar : MonoBehaviour
{
    ObjectPool<ProgressBar> pool;
    public Interactable owner;
    public Slider slider;
    public void SetPool(ObjectPool<ProgressBar> pool)
    {
        this.pool = pool;
    }
    public void Init(Vector3 position, Interactable owner)
    {
        transform.position = Camera.main.WorldToScreenPoint(position);
        this.owner = owner;
    }
    public void ReturnToPool()
    {
        pool.Release(this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
