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
    private bool canAttack = true;
    private bool isJumpingFromStanding = false;
    [SerializeField] private Staminabar staminabar; // Reference to Staminabar script
    public Spawner spawner;

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
        //throwPoint = transform.Find("Spawner"); 
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        staminabar = GetComponent<Staminabar>();
        if (staminabar == null)
        {
            staminabar = FindObjectOfType<Staminabar>();

            if (staminabar == null)
            {
                Debug.LogError("Staminabar not found in the scene.");
            }
        }


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
    if (sprintAction.triggered && characterController.isGrounded && staminabar.GetCurrentStamina() >= 20f)
    {
        isSprinting = true;
    }
    
    if (moveInput == 0 && moveBackwardInput == 0)
    {
        isSprinting = false;
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

    // Jump when the jump key is pressed and not sprinting
    if (jumpAction.triggered && characterController.isGrounded && !isSprinting)
    {
        velocity.y = Mathf.Sqrt(2 * jumpForce * gravity);
        animator.SetBool("isJumping", !characterController.isGrounded);

        isJumpingFromStanding = Mathf.Abs(moveInput + moveBackwardInput) < 0.1f;
        if (isJumpingFromStanding)
            moveAction.Disable();
        StartCoroutine(EnableMoveActionAfterDelay(1.0f));

    }

    // Attack when the attack key is pressed
    if (attackAction.triggered && canAttack)
    {
        if (characterController.isGrounded) StartCoroutine(AttackAnimation());
        //else StartCoroutine(AttackOnAir());

    }
    // Apply gravity
    velocity.y -= gravity * Time.deltaTime;

    // Move the character and set isRunning and isSprinting animation parameters
    characterController.Move((moveDirection.normalized * currentSpeed + velocity) * Time.deltaTime);

    // Set isRunning and isJumping animation parameters
    bool isRunning = Mathf.Abs(moveInput + moveBackwardInput) > 0.1f && characterController.isGrounded;
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
        canAttack = false;
        if (characterController.isGrounded && !animator.GetBool("isJumping"))
        {
        Debug.Log("Attacking...");
        animator.SetBool("isAttacking", true);
        moveAction.Disable();
        animator.speed = 1.0f / 0.2f;
        
        spawner.SpawnFireball();

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("isAttacking", false);
        animator.speed = 1.0f;
        moveAction.Enable();
        yield return new WaitForSeconds(0.2f);
        }
        canAttack = true;

    }
    IEnumerator EnableMoveActionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Re-enable moveAction after the specified delay
        if (!moveAction.enabled)
            moveAction.Enable();
    }
}
    private void LateUpdate()
        {

            // Check if stamina is below a certain threshold and stop sprinting
            if (staminabar.GetCurrentStamina() < 1f)
            {
                StopSprinting();
            }
        }

    // Method to handle the "canceled" event for sprint action
    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }
    public bool IsSprinting()
    {
    return isSprinting;
    }
    
    public void SetSprinting(bool sprinting)
    {
        isSprinting = sprinting;
    }
    private void StopSprinting()
    {
        // Stop sprinting logic goes here
        isSprinting = false;
    }

}