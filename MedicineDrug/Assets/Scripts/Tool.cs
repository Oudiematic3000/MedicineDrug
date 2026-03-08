using UnityEngine;

public class Tool : Usable
{
    public bool droppableOnFloor=false;

    public virtual void OnPickup(Player player)
    {
        if (template.emptyHandRequired && player.heldTool) return;
        if (template.toolNeeded != null && template.toolNeeded != player.heldTool) return;
    }

    public virtual void OnPutDown(Player player)
    {

    }
    public void OnPickup()
    {
        print("Interact Time is:" + template.interactTime);
    }
}
