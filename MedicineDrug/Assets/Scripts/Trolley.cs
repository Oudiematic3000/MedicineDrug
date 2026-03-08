using System;
using UnityEngine;

public class Trolley : Tool
{
    [SerializeField] public Transform trolleyParent, toolSlot;
    [SerializeField] GameObject trolleyBody;
    public float rotationSpeed, moveSpeed, acceleration;
    public WheelCollider[] wheels;
    public bool held;
    public HingeJoint joint;
    public Rigidbody trolleyRB;
    public Player holdingPlayer;
    public bool moving;
    public AudioClip movingSound;

    public TrolleyJointSettings jointSettings;

    public void Start()
    {
        
    }
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
        foreach(var wheel in wheels){wheel.enabled = true;}
    }

    public void AttachTrolley()
    {
        holdingPlayer.transform.rotation = Quaternion.LookRotation(trolleyBody.transform.forward*-1f, Vector3.up);

        float grabDistance = -0.8f;
        Vector3 targetPosition = trolleyBody.transform.position - trolleyBody.transform.forward * grabDistance;
        targetPosition.y = holdingPlayer.transform.position.y;

        holdingPlayer.transform.position = targetPosition;
        joint = trolleyParent.gameObject.AddComponent<HingeJoint>();
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

    [SerializeField] Transform playerAnchor;
    [SerializeField] Transform trolleyAnchor;
    [SerializeField] float maxDistance = 1.2f;

    public AudioSource sound;
    void FixedUpdate()
    {
        if (sound == null || !sound.gameObject.activeSelf) sound = AudioManager.instance.PlayLoopingWhile(movingSound, ()=>moving);
        
        if (trolleyRB.linearVelocity.magnitude > 0.1)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        
        if (!holdingPlayer) return;
        playerAnchor=holdingPlayer.physicsHandle.transform;
        trolleyAnchor = transform;
        Vector3 playerPos = playerAnchor.position;
        Vector3 trolleyPos = trolleyAnchor.position;

        Vector3 delta = playerPos - trolleyPos;
        float distance = delta.magnitude;

        if (distance > maxDistance)
        {
            Vector3 correctedPos = trolleyPos + delta.normalized * maxDistance;

            Vector3 offset = correctedPos - playerPos;

            holdingPlayer.rb.position += offset;
        }
    }
}

[System.Serializable]
public class TrolleyJointSettings
{
    [Header("Anchor")]
    public Vector3 anchor = new Vector3(0f, 1.38f, 1.05f);
    public Vector3 axis = Vector3.up;

    [Header("Connected Anchor")]
    public bool autoConfigureConnectedAnchor = false;
    public Vector3 connectedAnchor = new Vector3(0f, 0.1f, 0.09f);

    [Header("Spring")]
    public bool useSpring = true;
    public float spring = 10000f;
    public float damper = 2000f;
    public float targetPosition = 0f;

    [Header("Motor")]
    public bool useMotor = false;

    [Header("Limits")]
    public bool useLimits = true;
    public bool extendedLimits = true;
    public float minLimit = -40f;
    public float maxLimit = 40f;
    public float bounciness = 1f;
    public float bounceMinVelocity = 0.2f;
    public float contactDistance = 1.6f;
}
