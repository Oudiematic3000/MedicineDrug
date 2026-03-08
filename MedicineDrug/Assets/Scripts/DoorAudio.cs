using System;
using UnityEngine;

public class DoorAudio : MonoBehaviour
{
    public AudioClip openSound;
    public AudioClip slamSound;
    
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Door collider was hit");
        
        if (other.gameObject.GetComponentInChildren<Player>() != null)
        {
            Debug.Log("Player opened door");
            AudioManager.instance.PlaySFX(openSound);
        } else if (other.gameObject.GetComponentInChildren<Trolley>() != null || other.gameObject.GetComponentInChildren<GurneyHandle>())
        {
            Debug.Log("Gurney slammed door");
            AudioManager.instance.PlaySFX(slamSound);
        }
    }
}
