using UnityEngine;

public class Billboard : MonoBehaviour
{
    public bool alignOrLookAt = true;
    void Start()
    {
        
    }

    void Update()
    {
        if(alignOrLookAt)AlignWithCamera();
        else
        LookAtCamera();
    }

    void LookAtCamera()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
    void AlignWithCamera()
    {
        transform.forward=Camera.main.transform.forward;
    }
}
