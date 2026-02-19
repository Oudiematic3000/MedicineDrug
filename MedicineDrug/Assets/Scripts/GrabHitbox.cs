using UnityEngine;

public class GrabHitbox : MonoBehaviour
{
    Player player;
    [SerializeField] Material highlightMaterial;
    public Usable highlightedUsable;
    public GameObject usableGO;
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
        usableGO=usable.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        Usable usable;
        if (!other.GetComponent<Usable>()) return;
        else usable = other.GetComponent<Usable>();
        usable.objectRenderer.material=usable.originalMaterial;
        highlightedUsable = null;
        usableGO = null;
    }

    public void PickupAction()
    {
        if (usableGO.GetComponent<Tool>())
        {
            Tool tool = usableGO.GetComponent<Tool>();
            tool.OnPickup();
        }
    }

    public void InteractAction(bool Action)
    {
        if (usableGO.GetComponent<Interactable>()) {
            Interactable interactable = usableGO.GetComponent<Interactable>();
            interactable.OnInteract(Action); 
        }
    }
}
