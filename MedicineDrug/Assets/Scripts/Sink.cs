using UnityEngine;

public class Sink : Interactable
{
    private Player player;
    
    public override void OnInteract(bool action, Player player)
    {
        base.OnInteract(action, player);
        this.player = player;
    }
    
    
    public override void OnComplete()
    {
        player.GetComponent<Player>().makeClean();
    }
}
