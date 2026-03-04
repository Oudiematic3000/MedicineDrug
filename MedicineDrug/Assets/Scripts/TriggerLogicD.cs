using UnityEngine;

public class TriggerLogicD : MonoBehaviour
{
    public Collider triggeredCollider;
    public bool objectPresent = false;

    private void OnTriggerEnter(Collider other)
    {
        triggeredCollider = other;
        objectPresent = true;
    }

    private void OnTriggerExit(Collider other)
    {
        triggeredCollider = null;
        objectPresent = false;
    }
}
