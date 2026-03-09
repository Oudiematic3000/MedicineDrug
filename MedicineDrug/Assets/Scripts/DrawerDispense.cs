using System;
using UnityEngine;

public class DrawerDispense : MonoBehaviour
{
    public bool trolleyPresent = false;
    public GameObject trigger;
    public Collider presentObject;
    public Tool toolToSpawn;
    public AudioClip interactSound;
    public Player player;
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        
    }

    public void Subscribe(Player player)
    {
        this.player = player;
        this.player.oninteract += spawnTool;
    }
    public void Unsubscribe()
    {
        if(this.player != null)
        player.oninteract -= spawnTool;
    }

    public void Update()
    {
        if (trigger.GetComponent<TriggerLogicD>().objectPresent)
        {
            presentObject = trigger.GetComponent<TriggerLogicD>().triggeredCollider;
        
            if (presentObject.GetComponentInChildren<Trolley>())
            {
                trolleyPresent = true;
            }
            
        }
        else
        {
            trolleyPresent = false;
        }
    }

    private void spawnTool()
    {
        if (trolleyPresent && presentObject.GetComponentInChildren<Trolley>().holdingPlayer)
        
        {
            Surface surface = presentObject.GetComponentInChildren<Surface>();
            if (surface != null)
            {
                if (surface.placedTool != null) return;
                var go = Instantiate(toolToSpawn, surface.toolSlot);
                go.transform.localPosition = Vector3.zero;
                surface.placedTool = go;
                AudioManager.instance.PlaySFX(interactSound);
            }
        }
    }
}
