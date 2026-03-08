using UnityEngine;

public class Tool : Usable
{
    public bool droppableOnFloor=false;

    public virtual bool OnPickup(Player player)
    {
        if (template.emptyHandRequired && player.heldTool) { print("emptyneeded and not Empty"); return false; }
        if (template.toolNeeded != null && template.toolNeeded != player.heldTool) { print("toolNeeded and Tool Mismatched"); return false; }
        player.heldTool = this;
        return true;

    }

    public virtual void OnPutDown(Player player)
    {

    }
    public void OnPickup()
    {
        print("Interact Time is:" + template.interactTime);
    }
}
