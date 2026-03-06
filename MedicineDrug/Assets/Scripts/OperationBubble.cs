using UnityEngine;
using UnityEngine.UI;

public class OperationBubble : MonoBehaviour
{
    public UsableTemplate toolNeeded;
    public float timerLength = 10;
    float currentTime=0;
    public bool running=false;
    public Slider slider;
    public OperationQueueUI queueUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            if (currentTime <timerLength)
            {
                currentTime += Time.deltaTime;
            }
            else if (currentTime >=timerLength) 
            {
                OnTimeOut();
            }
            slider.value = currentTime / timerLength;
        }
    }
    public void Run()
    {
        running = true;
    }
    public void OnTimeOut()
    {
        queueUI.DequeueOperation();
    }
}
