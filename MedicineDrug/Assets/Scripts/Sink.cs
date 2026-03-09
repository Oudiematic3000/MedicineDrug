using UnityEngine;

public class Sink : Interactable
{
    private Player player;
    
    public override void OnInteract(bool action, Player player)
    {
        if (!player.isDirty) return;
        this.player = player;
        base.OnInteract(action, player);

    }


    public override void OnComplete()
    {
        if (player == null) return;
        player.ResetOperation();
        player.sparkleEffect();
    }
}
