using System;
using UnityEngine;
using UnityEngine.TextCore;

public class GurneyHandle : Tool
{
  
    [SerializeField] Transform gurneyBody;
    [SerializeField] Transform holdOffset;

    public float rotationSpeed;
    public float moveSpeed;
    public float acceleration;

    public WheelCollider[] wheels;
    public Rigidbody gurneyRB;

    public bool held;
    public Player holdingPlayer;
    public int swapForward=1;

    public GurneyBody gurneyBodyScript;
    public static event Action<bool> OnGurneyPickup;
    public override bool OnPickup(Player player)
    {
        if (holdingPlayer) return false;
        if (gurneyBodyScript.patient.operationQueue)
        {
            if (gurneyBodyScript.patient.operationQueue.operationBubbles.Count > 0) return false;
        }
        if (!base.OnPickup(player)) return false;

       

        holdingPlayer = player;
        held = true;

        player.SetTemporaryMovement(rotationSpeed, moveSpeed, acceleration);

        AttachTrolley();
        DisableWheels();
        return true;    
    }

    public override void OnPutDown(Player player)
    {
        base.OnPutDown(player);

        DetachTrolley();

        EnableWheels();

        player.RevertMovementToDefault();

        holdingPlayer = null;
        held = false;
    }
  
    void DisableWheels()
    {
        foreach (var wheel in wheels)
            wheel.enabled = false;
        if (gurneyBody.TryGetComponent<Rigidbody>(out gurneyRB))
        {
            gurneyRB.linearVelocity = Vector3.zero;
            gurneyRB.angularVelocity = Vector3.zero;
        }
    }

    void EnableWheels()
    {
        foreach (var wheel in wheels)
            wheel.enabled = true;
    }
    public float grabDistance = -0.9f;

    void AttachTrolley()
    {
       

        if (gurneyBodyScript.inMachineSpace) gurneyBodyScript.UnsnapFromTrigger();

        if (gurneyBody.TryGetComponent<Rigidbody>(out gurneyRB)) Destroy(gurneyRB);

        OnGurneyPickup?.Invoke(true);
        holdingPlayer.transform.rotation =
            Quaternion.LookRotation(-gurneyBody.forward*swapForward, Vector3.up);


        Vector3 targetPosition =
            gurneyBody.position - gurneyBody.forward*swapForward * grabDistance;

        targetPosition.y = holdingPlayer.transform.position.y;

        holdingPlayer.transform.position = targetPosition;

        gurneyBody.SetParent(holdingPlayer.transform);

        if (holdOffset)
        {
            transform.position = holdOffset.position;
            transform.rotation = holdOffset.rotation;
        }
    }

    void DetachTrolley()
    {
        gurneyBody.SetParent(null);
        OnGurneyPickup?.Invoke(false);

        gurneyRB = gurneyBody.gameObject.AddComponent<Rigidbody>();
        gurneyRB.mass = 50f;

        if (holdingPlayer && holdingPlayer.physicsHandle)
        {
            gurneyRB.linearVelocity = holdingPlayer.physicsHandle.linearVelocity;
        }
        if (gurneyBodyScript.inMachineSpace) gurneyBodyScript.SnapToTrigger();
    }

    private void Update()
    {

    }
}


