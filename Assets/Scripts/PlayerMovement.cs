using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Speed of player keyboard-movement, in meters/second")]
    [SerializeField] float speed = 3.5f;
    [SerializeField] float sprintSpeedMultiplier = 2f; 
    [SerializeField] float rotationSpeed = 180f; 
    [SerializeField] float jumpForce = 1.0f; 
    [SerializeField] float gravity = 9.81f;
    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;
    [SerializeField] InputAction sprintAction;
    [SerializeField] InputAction attackAction;
    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    private bool isSprinting = false;

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        attackAction.Enable();

        // Register the method for the "canceled" event of sprint action
        sprintAction.canceled += OnSprintCanceled;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        attackAction.Disable();

        // Unregister the method for the "canceled" event of sprint action
        sprintAction.canceled -= OnSprintCanceled;
    }

    private void OnValidate()
    {
        if (moveAction == null)
            moveAction = new InputAction(type: InputActionType.Value);
        if (moveAction.bindings.Count == 0)
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");

        if (jumpAction == null)
        {
            jumpAction = new InputAction(type: InputActionType.Button);
            jumpAction.AddBinding("<Keyboard>/space");
        }

        if (sprintAction == null)
        {
            sprintAction = new InputAction(type: InputActionType.Button);
            sprintAction.AddBinding("<Keyboard>/leftShift");
        }

        if (attackAction == null)
        {
            attackAction = new InputAction(type: InputActionType.Button);
            attackAction.AddBinding("<Keyboard>/leftCtrl");
        }
    }

    private void Start()
{
    characterController = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();

    if (moveAction == null)
    {
        moveAction = new InputAction(type: InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
    }
    
    // Enable the moveAction
    moveAction.Enable();
}


    private void Update()
{
   // Move forward only when the forward key is pressed
float moveInput = moveAction.ReadValue<Vector2>().y;
Vector3 moveDirection = transform.forward * moveInput;

// Move backward only when the backward key is pressed
float moveBackwardInput = moveAction.ReadValue<Vector2>().y; // Use positive value here
moveDirection += transform.forward * moveBackwardInput;

    // Sprinting
    if (sprintAction.triggered)
    {
        isSprinting = true;
    }

    // Apply sprint speed multiplier if sprinting
    float currentSpeed = isSprinting ? speed * sprintSpeedMultiplier : speed;

    // Rotate right continuously when the right key is held down
    if (moveAction.ReadValue<Vector2>().x > 0.1f)
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    // Rotate left continuously when the left key is held down
    if (moveAction.ReadValue<Vector2>().x < -0.1f)
    {
        transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
    }

    // Jump when the jump key is pressed
    if (jumpAction.triggered && characterController.isGrounded)
    {
        velocity.y = Mathf.Sqrt(2 * jumpForce * gravity);
        animator.SetBool("isJumping", true);
    }

    // Attack when the attack key is pressed
    if (attackAction.triggered)
        StartCoroutine(AttackAnimation());

    // Apply gravity
    velocity.y -= gravity * Time.deltaTime;

    // Move the character and set isRunning and isSprinting animation parameters
    characterController.Move((moveDirection.normalized * currentSpeed + velocity) * Time.deltaTime);

    // Set isRunning and isJumping animation parameters
    bool isRunning = Mathf.Abs(moveInput + moveBackwardInput) > 0.1f;
    animator.SetBool("isRunning", isRunning);
    animator.SetBool("isJumping", !characterController.isGrounded);
    animator.SetBool("isSprinting", isSprinting);

    // Clamp the character to the ground
    if (characterController.isGrounded && velocity.y < 0)
    {
        velocity.y = -2f;
        animator.SetBool("isJumping", false);
    }

    IEnumerator AttackAnimation()
    {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isAttacking", false);
    }
}


    // Method to handle the "canceled" event for sprint action
    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }
}
