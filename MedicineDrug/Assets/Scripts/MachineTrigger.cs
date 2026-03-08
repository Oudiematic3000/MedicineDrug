using UnityEngine;

public class MachineTrigger : MonoBehaviour
{
    public GameObject[] triggerFloor;
    public BoxCollider[] boxColliders;
    public GurneyBody gurneyBody;
    public MachineTriggerChild[] childTriggers;
    public bool gurneyHeld=false;
    private void OnEnable()
    {
         GurneyHandle.OnGurneyPickup +=SetGurneyHeld;
    }
    private void OnDisable()
    {
        GurneyHandle.OnGurneyPickup -=SetGurneyHeld;
    }
    void Start()
    {

    }
    public void SetGurneyHeld(bool held)
    {
        gurneyHeld = held;
    }
    void Update()
    {
        if (gurneyHeld)
        {
            if (CheckConnection())
            {
                ShowTrigger(1);
            }
            else
            {
                ShowTrigger(0);
            }
        }
        else
        {
            ShowTrigger(2);
        }
    }

    public void ShowTrigger(int show)
    {
        switch (show)
        {
            case 0:
                triggerFloor[0].SetActive(true);
                triggerFloor[1].SetActive(false);
                break;
            case 1:
                triggerFloor[0].SetActive(false);
                triggerFloor[1].SetActive(true);
                break;
            case 2:
                triggerFloor[0].SetActive(false);
                triggerFloor[1].SetActive(false);
                break;
        }
    }

    public bool CheckConnection()
    {
        GurneyBody localGurneyBody=null;
        bool matchFound = false;
        foreach (var trigger in childTriggers)
        {
            if (trigger.gurneyBody != null)
            {

                if (trigger.gurneyBody == localGurneyBody)
                {
                    matchFound = true;
                    gurneyBody = localGurneyBody;
                    gurneyBody.inMachineSpace = true;
                    gurneyBody.machineTrigger = this;
                    break;
                }
                else
                {
                    localGurneyBody = trigger.gurneyBody;
                }
            }
        }
        if (!matchFound)
        {
            if (gurneyBody)
            {
                gurneyBody.inMachineSpace = false;
                gurneyBody.machineTrigger = null;
            }
          gurneyBody=null;
            
        }
        return matchFound;
    }
}
