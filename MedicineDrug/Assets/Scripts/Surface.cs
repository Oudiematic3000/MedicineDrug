using UnityEngine;

public class Surface : Usable
{
    public Tool placedTool;
    public Transform toolSlot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpPutDown(Player player)
    {
        if (placedTool) { Pickup(player); print("surfacepickup"); }
        else { Putdown(player); print("putdown"); }
    }
    void Pickup(Player player)
    {
        if(player.heldTool)return;

        placedTool.transform.SetParent(player.hand);
        placedTool.transform.localPosition = Vector3.zero;
        placedTool.transform.rotation =Quaternion.LookRotation(player.hand.forward, Vector3.up);
        player.heldTool = placedTool;
        placedTool = null;
    }
    void Putdown(Player player)
    {
        if (!player.heldTool) return;
        print("actuallyPutdown");
        player.heldTool.transform.SetParent(toolSlot);
        player.heldTool.transform.localPosition = Vector3.zero;
        player.transform.rotation = Quaternion.LookRotation(toolSlot.forward, Vector3.up);
        placedTool=player.heldTool;
        player.heldTool = null;
    }
}
