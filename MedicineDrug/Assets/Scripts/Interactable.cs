using UnityEngine;
using UnityEngine.UI;

public class Interactable : Usable
{
    
    public bool interacting=false, completed=false, receivingInteractAction;
    public float progress=0f;
    public ProgressBar progressBar;

    private void Start()
    {
        
    }
    public virtual void OnInteract(bool action, Player player)
    {
        if (template.emptyHandRequired && player.heldTool) return;
        if (template.toolNeeded != null &&template.toolNeeded!=player.heldTool) return;

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

        if (interacting && progress<=template.interactTime)
        {
            progress += Time.deltaTime;
            
        }else if(!interacting && progress < template.interactTime && progress>0)
        {
            progress -= Time.deltaTime/1.5f;
        }
        else if(progress>=template.interactTime)
        {
            progress = template.interactTime;
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
        else if (progress <= 0)
        {
            if (!progressBar) return;
            progressBar.ReturnToPool();
            progressBar = null;
        }
        if (progressBar != null)
        progressBar.slider.value=(progress/template.interactTime);
    }

    public virtual void OnComplete()
    {

    }

}
