using UnityEngine;

public class Sink : Interactable
{
    private Player player;
    
    public override void OnInteract(bool action, Player player)
    {
        if (!player.isDirty) return;
        base.OnInteract(action, player);
        this.player = player;
    }
    
    
    public override void OnComplete()
    {
        player.ResetOperation();
        player.sparkleEffect();
    }
}
