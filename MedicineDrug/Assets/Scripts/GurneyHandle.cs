using UnityEngine;

public class GurneyHandle : Tool
{
    [SerializeField] public Transform gurneyParent;
    [SerializeField] GameObject gurneyBody;
    public float rotationSpeed, moveSpeed, acceleration;
    public WheelCollider[] wheels;
    public bool held;
    public HingeJoint joint;
    public Rigidbody gurneyRB;
    public Player holdingPlayer;
    [SerializeField] Vector3 grabDirection;
    [SerializeField] float grabDistance;

    public TrolleyJointSettings jointSettings;
    public override void OnPickup(Player player)
    {
        if (holdingPlayer) return;
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
        DetachTrolley();
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
        foreach (var wheel in wheels) { wheel.enabled = true; }
    }

    public void AttachTrolley()
    {

        
        Vector3 targetPosition = gurneyParent.transform.position - gurneyParent.forward * grabDirection.z* grabDistance;
        targetPosition.y = holdingPlayer.transform.position.y;

        holdingPlayer.transform.position = targetPosition;
        holdingPlayer.transform.rotation = Quaternion.LookRotation(gurneyParent.forward * grabDirection.z, Vector3.up);

        joint = gurneyParent.gameObject.AddComponent<HingeJoint>();
        joint.connectedBody = holdingPlayer.physicsHandle;
        joint.anchor = jointSettings.anchor;
        joint.axis = jointSettings.axis;

        joint.autoConfigureConnectedAnchor = jointSettings.autoConfigureConnectedAnchor;
        joint.connectedAnchor = jointSettings.connectedAnchor;

        joint.useSpring = jointSettings.useSpring;

        JointSpring spring = new JointSpring();
        spring.spring = jointSettings.spring;
        spring.damper = jointSettings.damper;
        spring.targetPosition = jointSettings.targetPosition;
        joint.spring = spring;

        joint.useMotor = jointSettings.useMotor;

        joint.useLimits = jointSettings.useLimits;
        joint.extendedLimits = jointSettings.extendedLimits;

        JointLimits limits = new JointLimits();
        limits.min = jointSettings.minLimit;
        limits.max = jointSettings.maxLimit;
        limits.bounciness = jointSettings.bounciness;
        limits.bounceMinVelocity = jointSettings.bounceMinVelocity;
        limits.contactDistance = jointSettings.contactDistance;

        joint.limits = limits;
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


