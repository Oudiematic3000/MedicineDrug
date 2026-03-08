using UnityEngine;

public class GurneyBody : MonoBehaviour
{
    public Surgery patient;
    public GurneyHandle headHandle, feetHandle;
    public bool inMachineSpace = false, snapped=false;
    public MachineTrigger machineTrigger;
    
    public void SnapToTrigger()
    {
        transform.SetParent(machineTrigger.transform);
        GetComponent<Rigidbody>().isKinematic = true;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        snapped = true;
        patient.StartQueue();
    }
    public void UnsnapFromTrigger()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        snapped= false;
    }
}
