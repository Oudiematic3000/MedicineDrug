using UnityEngine;

public class TriggerLogicD : MonoBehaviour
{
    public Collider triggeredCollider;
    public bool objectPresent = false;
    public DrawerDispense parent;

    private void OnTriggerEnter(Collider other)
    {
        triggeredCollider = other;
        objectPresent = true;
        if (other.GetComponentInChildren<Trolley>())
        {
            if(!other.GetComponentInChildren<Trolley>().holdingPlayer)return;
            parent.Subscribe(other.GetComponentInChildren<Trolley>().holdingPlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggeredCollider = null;
        objectPresent = false;
        if (other.GetComponentInChildren<Trolley>())
        {
            parent.Unsubscribe();
        }
    }
}
