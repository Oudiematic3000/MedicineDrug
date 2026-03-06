using System;
using UnityEngine;

public class DrawerDispense : MonoBehaviour
{
    public bool trolleyPresent = false;
    public GameObject trigger;
    public Collider presentObject;
    public Tool toolToSpawn;

    private void OnEnable()
    {
        Player.oninteract += spawnTool;
    }

    private void OnDisable()
    {
        Player.oninteract -= spawnTool;
    }

    public void Update()
    {
        if (trigger.GetComponent<TriggerLogicD>().objectPresent)
        {
            presentObject = trigger.GetComponent<TriggerLogicD>().triggeredCollider;
        
            if (presentObject.name == "SurgTrolley")
            {
                trolleyPresent = true;
            }
        }
    }

    private void spawnTool()
    {
        if (trolleyPresent)
        
        {
            Surface surface = presentObject.GetComponentInChildren<Surface>();
            if(surface.placedTool != null)return;
           var go= Instantiate(toolToSpawn, surface.toolSlot);
            go.transform.localPosition = Vector3.zero;
           surface.placedTool = go;
        }
    }
}
