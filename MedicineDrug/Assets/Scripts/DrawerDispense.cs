using System;
using UnityEngine;

public class DrawerDispense : MonoBehaviour
{
    public bool trolleyPresent = false;
    public GameObject trigger;
    public Collider presentObject;
    public Tool toolToSpawn;

    public void Start()
    {
        trolleyPresent = true; //Just for testing the instantiating right now
    }

    public void Update()
    {
        if (trigger.GetComponent<TriggerLogicD>().objectPresent)
        {
            presentObject = trigger.GetComponent<TriggerLogicD>().triggeredCollider;
        
            if (presentObject.name == "Trolley")
            {
                trolleyPresent = true;
            }
        }

        if (trolleyPresent)
        {
            Instantiate(toolToSpawn);
        }
    }
}
