using System;
using UnityEngine;

public class Trolley : Tool
{
    [SerializeField] Transform trolleyBody;
    [SerializeField] Transform holdOffset;

    public float rotationSpeed;
    public float moveSpeed;
    public float acceleration;

    public WheelCollider[] wheels;
    public Rigidbody trolleyRB;

    public bool held;
    public Player holdingPlayer;
    public bool moving;
    public AudioClip movingSound;



    public override bool OnPickup(Player player)
    {
        if (holdingPlayer) return false;

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

        trolleyRB.linearVelocity = Vector3.zero;
        trolleyRB.angularVelocity = Vector3.zero;
    }

    void EnableWheels()
    {
        foreach (var wheel in wheels)
            wheel.enabled = true;
    }
    public float grabDistance = -0.9f;

    void AttachTrolley()
    {
        Destroy(trolleyRB);

        holdingPlayer.transform.rotation =
            Quaternion.LookRotation(-trolleyBody.forward, Vector3.up);


        Vector3 targetPosition =
            trolleyBody.position - trolleyBody.forward * grabDistance;

        targetPosition.y = holdingPlayer.transform.position.y;

        holdingPlayer.transform.position = targetPosition;

        trolleyBody.parent.SetParent(holdingPlayer.transform);

        if (holdOffset)
        {
            transform.position = holdOffset.position;
            transform.rotation = holdOffset.rotation;
        }
    }

    public AudioSource sound;
    void FixedUpdate()
    {
        if (sound == null || !sound.gameObject.activeSelf) sound = AudioManager.instance.PlayLoopingWhile(movingSound, ()=>moving);
        if (!trolleyRB)
        {
            if (holdingPlayer.rb.linearVelocity.magnitude > 0.1)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
        }
        else
        {
            if (trolleyRB.linearVelocity.magnitude > 0.1)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
        }
        
        if (!holdingPlayer) return;
    }
    void DetachTrolley()
    {
        trolleyBody.parent.SetParent(null);

        trolleyRB = trolleyBody.parent.gameObject.AddComponent<Rigidbody>();
        trolleyRB.mass = 50f;
        trolleyRB.constraints = RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationZ;
        

        if (holdingPlayer && holdingPlayer.physicsHandle)
        {
            trolleyRB.linearVelocity = holdingPlayer.physicsHandle.linearVelocity;
        }
    }
}