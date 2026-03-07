using UnityEngine;

public class Surface : Usable
{
    public Tool placedTool;
    public Transform toolSlot;
    public Animator playerAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpPutDown(Player player)
    {
        if (placedTool) Pickup(player);
        else Putdown(player);
    }
    void Pickup(Player player)
    {
        playerAnimator.SetBool("holding", true);
        Debug.Log("Pickup in surface fired");
        Debug.Log("Holding set to true");
        
        if(player.heldTool)return;

        placedTool.transform.SetParent(player.hand);
        placedTool.transform.localPosition = Vector3.zero;
        placedTool.transform.rotation =Quaternion.LookRotation(player.hand.forward, Vector3.up);
        placedTool = null;
        
    }
    void Putdown(Player player)
    {
        playerAnimator.SetBool("holding", false);
        Debug.Log("Putdown in surface fired");
        Debug.Log("Holding set to false");
        
        if (!player.heldTool) return;
        player.heldTool.transform.SetParent(toolSlot);
        player.heldTool.transform.localPosition = Vector3.zero;
        player.transform.rotation = Quaternion.LookRotation(toolSlot.forward, Vector3.up);
        player.heldTool = null;
    }
}
