using UnityEngine;

public class Interactable : Usable
{
    public float interactTime;
    public Tool neededTool;
    public bool interacting=false;
    float progress=0f;
    public void OnInteract(bool action)
    {
        interacting = action;
    }
    private void Update()
    {
        if (interacting && progress<=interactTime)
        {
            progress += Time.deltaTime;
            Debug.Log("Progress" + progress);
        }else if(!interacting && progress < interactTime &&progress>0)
        {
            progress -= Time.deltaTime/1.5f;
        }
        else
        {
            OnComplete();
            Debug.Log("Complete");
        }
    }

    void OnComplete()
    {
        //SO action
    }

}
