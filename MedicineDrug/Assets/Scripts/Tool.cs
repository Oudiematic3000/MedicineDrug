using UnityEngine;

public class Tool : Usable
{
    [SerializeField] public UsableTemplate template;
    
    public void OnPickup()
    {
        print("Interact Time is:" + template.interactTime);
    }
}
