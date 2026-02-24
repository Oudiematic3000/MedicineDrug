using UnityEngine;

public class Trolley : Tool
{
    [SerializeField] Transform trolleyParent;
    public float rotationSpeed, moveSpeed, acceleration;
    public WheelCollider[] wheels;
    public bool held;
    public ConfigurableJoint joint;
    public Rigidbody trolleyRB;
    public Player holdingPlayer;
    public override void OnPickup(Player player)
    {
        base.OnPickup(player);

       // trolleyParent.SetParent(player.hand);                   //adding to hand here until base functionality is done
        //trolleyParent.localPosition = Vector3.zero;
        player.SetTemporaryMovement(rotationSpeed, moveSpeed, acceleration);
        DisableWheels();
        joint.connectedBody = player.physicsHandle;
        holdingPlayer = player;
        //joint.projectionMode = JointProjectionMode.PositionAndRotation;
        //joint.projectionDistance = 0.05f;
        //joint.projectionAngle = 5f;

        JointDrive drive = new JointDrive();
        drive.positionSpring = 4000f;
        drive.positionDamper = 250f;
        drive.maximumForce = 10000f;

        joint.xDrive = drive;
        joint.yDrive = drive;
        joint.zDrive = drive;
        held = true;
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

        Rigidbody rb = GetComponentInParent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
       // rb.isKinematic = true;
        rb.detectCollisions = true;
      

    }

    void EnableWheels()
    {
        foreach(var wheel in wheels){wheel.enabled = true;}
        GetComponent<Rigidbody>().isKinematic = false;  
    }
   
    private void Update()
    {
       
    }
}
