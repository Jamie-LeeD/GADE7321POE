using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float rotationSpeed = 10f;
    public float jumpForce = 8f;
    public float gravity = -20f;
    public float airControl = 0.5f;

    public Transform cameraTransform;
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundLayer;

    public int maxJumps = 2;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 moveDirection;

    private bool isGrounded;
    private int jumpCount;

    [Header("Sprint Settings")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 10f;
    public float stamina = 5f;
    public float maxStamina = 5f;
    public float staminaDrainRate = 1.5f;
    public float staminaRegenRate = 1f;

    private bool isSprinting;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        CheckGround();
        HandleSprint();    
        HandleMovement();  
        HandleGravity();
        MoveCharacter();
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCount = 0;
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 inputDirection = (forward * vertical + right * horizontal);
        inputDirection = Vector3.ClampMagnitude(inputDirection, 1f);

        if (isGrounded)
        {
            float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
            moveDirection = inputDirection * currentSpeed;
        }
        else
        {
            Vector3 airMove = inputDirection * moveSpeed * airControl;
            moveDirection = Vector3.Lerp(moveDirection, airMove, Time.deltaTime);
        }

        // Smooth rotation
        if (inputDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            velocity.y = jumpForce;
            jumpCount++;
        }
    }

    void HandleSprint()
    {
        bool sprintInput = Input.GetKey(KeyCode.LeftShift);

        // Can sprint only if moving, grounded, and has stamina
        if (sprintInput && stamina > 0f && moveDirection.magnitude > 0.1f && isGrounded)
        {
            isSprinting = true;
            stamina -= staminaDrainRate * Time.deltaTime;
        }
        else
        {
            isSprinting = false;
            stamina += staminaRegenRate * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
    }
    void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;
    }

    void MoveCharacter()
    {
        Vector3 finalMove = moveDirection + velocity;
        controller.Move(finalMove * Time.deltaTime);
    }
}