using UnityEngine;

public class GrabHitbox : MonoBehaviour
{
    Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Usable usable;
        if (!other.GetComponent<Usable>()) return;
        else usable = other.GetComponent<Usable>();
        usable.OnPickup();
    }
}
