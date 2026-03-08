using System;
using UnityEngine;

public class DoorAudio : MonoBehaviour
{
    public AudioClip openSound;
    public AudioClip slamSound;
    public bool cooldown=false;
    public void OnCollisionEnter(Collision other)
    {
        if (cooldown) return;

        if (other.gameObject.GetComponentInChildren<Player>() != null)
        {
            AudioManager.instance.PlaySFX(openSound, 0.3f);
            
        } else if (other.gameObject.GetComponentInChildren<Trolley>() != null || other.gameObject.GetComponentInChildren<GurneyHandle>())
        {
            AudioManager.instance.PlaySFX(slamSound, 0.3f);
        }
        cooldown = true;
        LeanTween.delayedCall(0.8f,() => { cooldown = false; });
    }
}
