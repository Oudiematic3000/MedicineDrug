using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public bool score;
    public AudioClip[] screams;
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponentInChildren<GurneyBody>()) return;
        if (score)
        {
            GameManager.instance.AddScore();
        }
        else
        {
            AudioManager.instance.PlaySFX(screams[Random.Range(0,screams.Length)],0.2f);
        }
    }
}
