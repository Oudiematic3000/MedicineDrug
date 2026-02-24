using UnityEngine;
using UnityEngine.UI;

public class Interactable : Usable
{
    public float interactTime;
    public Tool neededTool;
    public bool interacting=false, completed=false, receivingInteractAction;
    public float progress=0f;
    public ProgressBar progressBar;

    private void Start()
    {
        
    }
    public void OnInteract(bool action)
    {
        if (!completed)
        {
            interacting = action;
            if (progressBar) return;
            progressBar = ProgressBarManager.instance.GetBar();
            progressBar.Init(transform.position + (Vector3.up * 1.5f), this);
        }
    }
    private void Update()
    {

        if (completed) return;

        if (interacting && progress<=interactTime)
        {
            progress += Time.deltaTime;
            
        }else if(!interacting && progress < interactTime && progress>0)
        {
            progress -= Time.deltaTime/1.5f;
        }
        else if(progress>=interactTime)
        {
            progress = interactTime;
            OnComplete();
            Debug.Log("complete"+name);
            completed = true;
            progress = 0f;
            interacting=false;
            progressBar.ReturnToPool();
            progressBar = null;
            LeanTween.delayedCall(1f, () =>
            {
                completed= false;   
            });
        }
        if(progressBar != null)
        progressBar.slider.value=(progress/interactTime);
    }

    void OnComplete()
    {
        //SO action
    }

}
