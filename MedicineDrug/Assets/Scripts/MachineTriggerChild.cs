using UnityEngine;

public class MachineTriggerChild : MonoBehaviour
{
    [SerializeField]MachineTrigger parent;
    public GurneyBody gurneyBody;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<GurneyBody>())
        {
            gurneyBody = other.GetComponentInChildren<GurneyBody>();
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInChildren<GurneyBody>() == gurneyBody)
        {
            gurneyBody=null;
        }
    }
}
