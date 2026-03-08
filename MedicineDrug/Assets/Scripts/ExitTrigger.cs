using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    GurneyBody gurneyBody;
    private void OnTriggerEnter(Collider other)
    {
        print("triggered");
        if (other.GetComponentInChildren<GurneyBody>())
        {
            print("confirt");
            gurneyBody = other.GetComponentInChildren<GurneyBody>();
            gurneyBody.inExitSpace = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInChildren<GurneyBody>() == gurneyBody)
        {
            gurneyBody.inExitSpace=false;
            gurneyBody = null;
            
        }
    }
}
