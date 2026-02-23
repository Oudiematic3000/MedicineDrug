using UnityEngine;

public class Trolley : Tool
{
    [SerializeField] Transform trolleyParent;
    public float rotationSpeed, moveSpeed, acceleration;
    public WheelCollider[] wheels;
    public override void OnPickup(Player player)
    {
        base.OnPickup(player);

        trolleyParent.SetParent(player.hand);                   //adding to hand here until base functionality is done
        //trolleyParent.localPosition = Vector3.zero;
        player.SetTemporaryMovement(rotationSpeed, moveSpeed, acceleration);
        DisableWheels();

    }

    public override void OnPutDown(Player player)
    {
        base.OnPutDown(player);
        trolleyParent.SetParent(null);
        player.RevertMovementToDefault();
        EnableWheels();
    }

    void DisableWheels()
    {
        foreach (var wheel in wheels) wheel.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.detectCollisions = false;
    }

    void EnableWheels()
    {
        foreach(var wheel in wheels){wheel.enabled = true;}
        GetComponent<Rigidbody>().isKinematic = false;  
    }
}
