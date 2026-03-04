using UnityEngine;

public class Trolley : Tool
{
    [SerializeField] Transform trolleyParent;
    [SerializeField] GameObject trolleyBody;
    public float rotationSpeed, moveSpeed, acceleration;
    public WheelCollider[] wheels;
    public bool held;
    public HingeJoint joint;
    public Rigidbody trolleyRB;
    public Player holdingPlayer;
    public override void OnPickup(Player player)
    {
        if(holdingPlayer != null)
        {
            DetachTrolley();
            OnPutDown(holdingPlayer);
            return;
        }
        base.OnPickup(player);
        
        player.SetTemporaryMovement(rotationSpeed, moveSpeed, acceleration);
        DisableWheels();
        holdingPlayer = player;
        held = true;
        AttachTrolley();
    }

    public override void OnPutDown(Player player)
    {
        base.OnPutDown(player);
        player.RevertMovementToDefault();
        EnableWheels();
        holdingPlayer = null;
        held = false;
    }

    void DisableWheels()
    {
        foreach (var wheel in wheels) wheel.enabled = false;

        Rigidbody rb = GetComponentInParent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.detectCollisions = true;
      

    }

    void EnableWheels()
    {
        foreach(var wheel in wheels){wheel.enabled = true;}
    }

    public void AttachTrolley()
    {
        holdingPlayer.transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);

        // 2. Move player behind trolley
        float grabDistance = 0.8f; // tweak as needed
        Vector3 targetPosition = transform.position - transform.forward * grabDistance;

        joint = trolleyBody.AddComponent<HingeJoint>();
        joint.connectedBody = holdingPlayer.physicsHandle;
        joint.anchor = new Vector3(0f, -0.56f, -0.77f);
        joint.axis = Vector3.up;

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = new Vector3(0f, 0f, 0.21f);

        joint.useSpring = true;
        JointSpring spring = new JointSpring();
        spring.spring = 10000f;
        spring.damper = 2000f;
        spring.targetPosition = 0f;
        joint.spring = spring;

        joint.useMotor = false;

        joint.useLimits = true;
        JointLimits limits = new JointLimits();
        limits.min = -40f;
        limits.max = 40f;
        limits.bounciness = 0f;
        limits.bounceMinVelocity = 0.2f;
        limits.contactDistance = 1.6f;
        joint.limits = limits;

        joint.extendedLimits = true;
    }

    public void DetachTrolley()
    {
        if (joint != null)
        {
            Destroy(joint);
           
        }
    }

    private void Update()
    {
       
    }
}
