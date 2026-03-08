using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

public class Player : MonoBehaviour, IGameplayActions
{
    public Rigidbody rb;
    [SerializeField] GrabHitbox grabHitbox;
    public Transform hand;
    public Animator anim;
    public Tool heldTool;
    public Rigidbody physicsHandle;
    Vector2 moveInput;
    public ParticleSystem dust;
    public ParticleSystem sparkle;
    public AudioClip sparkleSound;
    public int bottomRand, topRand;
    private int randomOpCount;
    private int numOperations;
    public Material dirtyPlayer;
    public Material cleanPlayer;
    public bool isDirty=false;
    public float rotationSpeed, moveSpeed, acceleration;
    float defaultRotationSpeed, defaultMoveSpeed, defaultAcceleration;
    public bool locked=false;

    public event Action oninteract;
    
    private void Awake()
    {
        sparkle.Pause();
        rb = GetComponent<Rigidbody>();
        defaultRotationSpeed = rotationSpeed;
        defaultMoveSpeed=moveSpeed;
        defaultAcceleration = acceleration;
        anim = GetComponentInChildren<Animator>();
    }
 
    void Start()
    {
        randomOpCount = UnityEngine.Random.Range(bottomRand, topRand+1);
        dust.Stop();
    }
    void Update()
    {
    }
    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude < 0.1)
        {
            dust.Stop();
        }
        
        Move();
        physicsHandle.MovePosition(hand.position);
        
        if (!heldTool)
        {
            anim.SetBool("holding", false);
            //Debug.Log("Holding set to false (From checking if the player has a tool)");
        }
        else if (heldTool)
        {
            anim.SetBool("holding", true);
            //Debug.Log("Holding set to true (From checking if the player has a tool)");
        }
    }

    public void sparkleEffect()
    {
        AudioManager.instance.PlaySFX(sparkleSound);
        sparkle.Play();
    }

    public void MakeDirty()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = dirtyPlayer;
    }

    public void MakeClean()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = cleanPlayer;
    }

    public void CheckDirty()
    {
        if (numOperations == randomOpCount)
        {
            MakeDirty();
            isDirty = true;
        }
    }

    public void IncrementOperation()
    {
        numOperations++;
        CheckDirty();
    }

    public void ResetOperation()
    {
        numOperations = 0;
        randomOpCount = UnityEngine.Random.Range(bottomRand, topRand+1);
        MakeClean();
        isDirty = false;
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            grabHitbox.InteractAction(true, this);
            anim.SetBool("using", true);
        }
        else if (context.canceled)
        {
            grabHitbox.InteractAction(false, this);
            anim.SetBool("using", false);
        }
        
        oninteract?.Invoke();
    }

    public void OnLock(InputAction.CallbackContext context)
    {
        if (context.performed) locked = true;
        else if(context.canceled)locked = false;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if(context.performed)
        moveInput = context.ReadValue<Vector2>();
        if (context.canceled) moveInput = Vector2.zero;
        dust.Play();
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if(!context.performed)return;
        if ((heldTool&& (!heldTool.GetComponentInChildren<Trolley>() && !heldTool.GetComponentInChildren<GurneyHandle>()))||!heldTool)
        {
            grabHitbox.PickupAction(this);
            anim.SetBool("holding", true);
            Debug.Log("Holding set to true");
        }
        else
        {
            if (heldTool.template.droppableOnFloor)
            {
                heldTool.OnPutDown(this);
                anim.SetBool("holding", false);
                heldTool = null;
            }
            else if(grabHitbox.usableGO.GetComponent<Surface>())
            {
                grabHitbox.PickupAction(this);
                anim.SetBool("holding", false);
                heldTool = null;

            }
        }
    }

    void Move()
    {
        anim.SetFloat("speed", moveInput.magnitude);
        if (moveInput == Vector2.zero) return;

        if (heldTool)
        {
            if (heldTool.GetComponentInChildren<Trolley>() || heldTool.GetComponentInChildren<GurneyHandle>())
            {
                float turnSpeed = 120f;
                float forwardSpeed = moveSpeed;

              
                    float turn = moveInput.x * turnSpeed * Time.deltaTime;
                    transform.Rotate(0, turn, 0);

                if (locked) forwardSpeed = 0;
                else forwardSpeed = moveSpeed;
                Vector3 forwardMove = transform.forward * moveInput.y * forwardSpeed;

                rb.linearVelocity = Vector3.MoveTowards(
                    rb.linearVelocity,
                    forwardMove,
                    acceleration * Time.deltaTime
                );
            }
            else
            {
                
            Vector3 moveInput3 = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(moveInput3);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

           if(!locked) rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, moveInput3 * moveSpeed, acceleration * Time.deltaTime);
            }
        }

        else
        {
            Vector3 moveInput3 = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(moveInput3);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if(!locked)rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, moveInput3 * moveSpeed, acceleration * Time.deltaTime);
        }
       
    }

    public void SetTemporaryMovement(float rotationTime, float moveSpeed, float accelerationTime)
    {
        this.rotationSpeed=rotationTime;
        this.moveSpeed=moveSpeed;
        this.acceleration=accelerationTime;
    }
    public void RevertMovementToDefault()
    {
        rotationSpeed = defaultRotationSpeed;
        moveSpeed = defaultMoveSpeed;
        acceleration = defaultAcceleration;
    }
}
