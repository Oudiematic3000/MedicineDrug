using UnityEngine;

public class AneMachine : Interactable
{
    public static AneMachine instance;
    public bool depleted=false;
    public float delta;
    public float tickDownModifier=1.5f;
    void Start()
    {
        progress = template.interactTime;
        if(!instance)instance = this;
        if (progressBar) return;
        progressBar = ProgressBarManager.instance.GetBar();
        progressBar.Init(transform.position + (Vector3.up * 1.5f), this);
    }

    // Update is called once per frame
    void Update()
    {
        if (completed) return;

        if (interacting && progress <= template.interactTime)
        {
            if (depleted)
            {
                delta = Time.deltaTime * 0.75f;
                progress += delta;
            }
            else
            {
                delta = Time.deltaTime;
                progress += delta;
            }

        }
        else if (!interacting && progress > 0)
        {
            delta= Time.deltaTime / tickDownModifier;
            progress -= delta;
        }
        else if (progress <= 0)
        {
            progress = 0;
            OnDeplete();
            depleted = true;

        }
        else if (progress >= template.interactTime && depleted)
        {
            depleted = false;
            progress = template.interactTime;
            OnComplete();
            completed = true;
            interacting = false;
            LeanTween.delayedCall(1f, () =>
            {
                completed = false;
            });
        }
        if (progressBar != null)
            progressBar.slider.value = (progress / template.interactTime);
    }
    public override void OnInteract(bool action, Player player)
    {
        if (player.heldTool != null)return;
            if (!completed)
            {
                interacting = action;
                
            }
    }
    public override void OnComplete()
    {
    }
    public void OnDeplete()
    {

    }
}
