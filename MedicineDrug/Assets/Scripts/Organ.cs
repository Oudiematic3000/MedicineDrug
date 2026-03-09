using UnityEngine;

public class Organ : MonoBehaviour
{
    void Start()
    {
        LeanTween.delayedCall(5f, () => { Destroy(gameObject); });
    }

}
