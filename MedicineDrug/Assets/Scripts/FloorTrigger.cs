using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public bool score;
    public AudioClip[] screams;
    public AudioClip pointSound;
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponentInChildren<GurneyBody>()) return;
        if (score)
        {
            GameManager.instance.AddScore();
            AudioManager.instance.PlaySFX(pointSound, 0.8f);
        }
        else
        {
            AudioManager.instance.PlaySFX(screams[Random.Range(0,screams.Length)],0.2f);
        }
    }
}
