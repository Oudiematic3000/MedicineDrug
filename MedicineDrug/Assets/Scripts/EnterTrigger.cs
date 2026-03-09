using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterTrigger : MonoBehaviour
{
    public List<GurneyBody> gurneyBodies = new List<GurneyBody>();
    public Image vignette;
    LTDescr loseTimer, vignetteTimer, vignetteLeaveTimer, graceTimer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GurneyBody>(out GurneyBody gurney))
        {
            if (!gurneyBodies.Contains(gurney))
            {
                gurneyBodies.Add(gurney);
                if (graceTimer != null)
                    LeanTween.cancel(graceTimer.uniqueId);
                graceTimer = LeanTween.delayedCall(0.8f, CheckGurneys);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<GurneyBody>(out GurneyBody gurney))
        {
            if (gurneyBodies.Contains(gurney))
            {
                gurneyBodies.Remove(gurney);
                CheckGurneys();
            }
        }
    }

    public void CheckGurneys()
    {
        if (gurneyBodies.Count > 0)
        {
            if (loseTimer == null) 
                StartLoseTimer();
        }
        else
        {
            if (loseTimer != null)
            {
                Debug.Log("LoseTimerCancelled");
                LeanTween.cancel(loseTimer.uniqueId);
                loseTimer = null;
            }

            if (vignetteTimer != null)
            {
                LeanTween.cancel(vignetteTimer.uniqueId);
                vignetteTimer = null;
            }

            vignetteLeaveTimer = LeanTween.value(vignette.color.a, 0, 1f)
                .setOnUpdate((float val) =>
                {
                    vignette.color = new Color(1, 1, 1, val);
                });
        }
    }
    public void StartLoseTimer()
    {
       vignetteTimer = LeanTween.value(vignette.color.a, 1, 5f).setOnUpdate((float val) => { vignette.color = new Color(1, 1, 1, val); });
       loseTimer = LeanTween.delayedCall(5f, GameManager.instance.Lose);
    }
}
