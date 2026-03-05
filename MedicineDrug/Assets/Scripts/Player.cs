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

    public float rotationSpeed, moveSpeed, acceleration;
    float defaultRotationSpeed, defaultMoveSpeed, defaultAcceleration;

    public static event Action oninteract;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        defaultRotationSpeed = rotationSpeed;
        defaultMoveSpeed=moveSpeed;
        defaultAcceleration = acceleration;
        anim = GetComponentInChildren<Animator>();
    }
 
    void Start()
    {
        
    }
    void Update()
    {
    }
    private void FixedUpdate()
    {
        Move();
        physicsHandle.MovePosition(hand.position);
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) grabHitbox.InteractAction(true, this);
        else
        if (context.canceled) grabHitbox.InteractAction(false, this);
        
        oninteract?.Invoke();
    }

    public void OnLock(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        
        if(context.performed)
        moveInput = context.ReadValue<Vector2>();
        if (context.canceled) moveInput = Vector2.zero;
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if(!context.performed)return;
        if (!heldTool)
        {
            grabHitbox.PickupAction(this);
        }
        else
        {
            heldTool.OnPutDown(this);
            heldTool=null;
        }
    }

    void Move()
    {
        anim.SetFloat("speed", moveInput.magnitude);
        if(moveInput==Vector2.zero)return;
        Vector3 moveInput3 = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(moveInput3);
        transform.rotation =Quaternion.Slerp(transform.rotation, targetRotation,rotationSpeed*Time.deltaTime);

        rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, moveInput3 * moveSpeed, acceleration*Time.deltaTime);
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
