using UnityEngine;

public class GrabHitbox : MonoBehaviour
{
    Player player;
    [SerializeField] Material highlightMaterial;
    public Usable highlightedUsable;
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
        usable.objectRenderer.material = highlightMaterial;
        highlightedUsable=usable;
    }
    private void OnTriggerExit(Collider other)
    {
        Usable usable;
        if (!other.GetComponent<Usable>()) return;
        else usable = other.GetComponent<Usable>();
        usable.objectRenderer.material=usable.originalMaterial;
        highlightedUsable = null;
    }

    public void PickupAction()
    {
        if (highlightedUsable is Tool) highlightedUsable.GetComponent<Tool>().OnPickup(); 
    }

    public void InteractAction(bool Action)
    {
        if(highlightedUsable is Interactable) highlightedUsable.GetComponent<Interactable>().OnInteract(Action);
    }
}
