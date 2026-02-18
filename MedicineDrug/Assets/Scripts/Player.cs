using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

public class Player : MonoBehaviour, IGameplayActions
{
    Rigidbody rb;
    [SerializeField] GrabHitbox grabHitbox;

    Vector2 moveInput;

    public float rotationTime, moveSpeed, accelerationTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
 
    void Start()
    {
        
    }
    void Update()
    {
        Move();
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) grabHitbox.InteractAction(true);
        if(context.canceled) grabHitbox.InteractAction(false);
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
        grabHitbox.PickupAction();
    }

    void Move()
    {
        if(moveInput==Vector2.zero)return;
        Vector3 moveInput3 = new Vector3(moveInput.x, 0, moveInput.y);
        Quaternion targetRotation = Quaternion.LookRotation(moveInput3);
        transform.rotation =Quaternion.Slerp(transform.rotation, targetRotation,rotationTime*Time.deltaTime);

        rb.linearVelocity = Vector3.Lerp(transform.forward, moveInput3 * moveSpeed, accelerationTime*Time.deltaTime);
    }
}
