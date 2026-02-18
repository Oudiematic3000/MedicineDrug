using UnityEngine;

public class Tool : Usable
{
    public override void OnPickup()
    {
        print("Picked up!");
    }
}
