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
        if (player != null)
        {
            player.oninteract -= spawnTool;
            player = null;
        }
    }

    void Update()
    {
        var triggerLogic = trigger.GetComponent<TriggerLogicD>();

        trolleyPresent = false;
        presentObject = null;

        if (!triggerLogic.objectPresent)
            return;

        var collider = triggerLogic.triggeredCollider;
        if (collider == null)
            return;

        var trolley = collider.GetComponentInChildren<Trolley>();
        if (trolley != null)
        {
            trolleyPresent = true;
            presentObject = collider;
        }
    }
    private void spawnTool()
    {
        if (!trolleyPresent) return;
        if (presentObject == null) return;
        Trolley trolley = presentObject.GetComponentInChildren<Trolley>();
        if (trolley == null) return;

        if (!trolley.holdingPlayer) return;

        Surface surface = presentObject.GetComponentInChildren<Surface>();
        if (surface == null) return;

        if (surface.placedTool != null) return;

        var go = Instantiate(toolToSpawn, surface.toolSlot);
        go.transform.localPosition = Vector3.zero;
        surface.placedTool = go;

        AudioManager.instance.PlaySFX(interactSound);
    }
}
