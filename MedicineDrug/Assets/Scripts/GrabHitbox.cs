using UnityEngine;

public class GrabHitbox : MonoBehaviour
{
    Player player;
    [SerializeField] Material highlightMaterial;
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
        usable.objectRenderer.material = highlightMaterial;
    }
    private void OnTriggerExit(Collider other)
    {
        Usable usable;
        if (!other.GetComponent<Usable>()) return;
        else usable = other.GetComponent<Usable>();
        usable.objectRenderer.material=usable.originalMaterial;
    }
}
