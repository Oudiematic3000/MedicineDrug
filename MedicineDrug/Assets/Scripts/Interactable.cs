using UnityEngine;

public class Interactable : Usable
{
    public float interactTime;
    public Tool neededTool;
    public bool interacting=false, completed=false;
    float progress=0f;
    public void OnInteract(bool action)
    {
        if(!completed)
        interacting = action;
    }
    private void Update()
    {
        if (completed) return;
        if (interacting && progress<=interactTime)
        {
            progress += Time.deltaTime;
            Debug.Log("Progress" + progress);
        }else if(!interacting && progress < interactTime &&progress>0)
        {
            progress -= Time.deltaTime/1.5f;
        }
        else if(progress>=interactTime)
        {
            OnComplete();
            Debug.Log("complete");
            completed = true;
            progress = 0f;
            interacting=false;
            LeanTween.delayedCall(1f, () =>
            {
                completed= false;   
            });
        }
    }

    void OnComplete()
    {
        //SO action
    }

}
