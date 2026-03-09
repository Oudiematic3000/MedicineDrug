using UnityEngine;

public class TriggerLogicD : MonoBehaviour
{
    public Collider triggeredCollider;
    public bool objectPresent = false;
    public DrawerDispense parent;
    public GameObject[] triggerFloor;
    public bool gurneyHeld = false;

    private void OnEnable()
    {
        Trolley.OnTrolleyHeld += SetGurneyHeld;
    }
    private void OnDisable()
    {
        Trolley.OnTrolleyHeld -= SetGurneyHeld;
    }
    void Start()
    {

    }
    void Update()
    {
        if (gurneyHeld)
        {
            if (objectPresent)
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
    public void SetGurneyHeld(bool held)
    {
        gurneyHeld = held;
    }
    private void OnTriggerEnter(Collider other)
    {
       // if (other.GetComponentInChildren<Player>()) return;

       
        //if (other.GetComponentInChildren<Trolley>())
        //{
        //    triggeredCollider = other;
        //    objectPresent = true;
        //    if (!other.GetComponentInChildren<Trolley>().holdingPlayer)return;
        //    parent.Subscribe(other.GetComponentInChildren<Trolley>().holdingPlayer);
        //}
        if (other.gameObject.CompareTag("Trolley"))
        {
            triggeredCollider = other;
            objectPresent = true;
            if (!other.GetComponentInChildren<Trolley>().holdingPlayer) return;
            parent.Subscribe(other.GetComponentInChildren<Trolley>().holdingPlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
       // if (other.GetComponentInChildren<Player>()) return;

        //if (other.GetComponentInChildren<Trolley>())
        //{
        //    triggeredCollider = null;
        //    objectPresent = false;
        //    parent.Unsubscribe();
        //}
        if (other.gameObject.CompareTag("Trolley"))
        {
            triggeredCollider = null;
            objectPresent = false;
            parent.Unsubscribe();
        }
    }
}
