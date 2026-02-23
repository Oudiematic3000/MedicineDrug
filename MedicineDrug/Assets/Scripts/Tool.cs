using UnityEngine;

public class Tool : Usable
{
    public bool droppableOnFloor=false;
    public virtual void OnPickup(Player player)
    {
        print("Picked up!");
    }

    public virtual void OnPutDown(Player player)
    {
        print("Picked up!");
    }
}
