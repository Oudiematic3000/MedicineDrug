using UnityEngine;

public class ThrowAway : Interactable
{
    private Player player;
    public override void OnInteract(bool action, Player player)
    {
        this.player = player;

        if (!this.player.heldTool)
        {
            player = null;
            return;
        }
        base.OnInteract(action, player);
      
    }

    public override void OnComplete()
    {
        base.OnComplete();
      
            Tool tool = player.heldTool;
            Destroy(tool.gameObject);
            player.heldTool = null;
        

    }
}
