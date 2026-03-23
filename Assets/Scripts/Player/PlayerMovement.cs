using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private CharacterController controller;
    private PlayerInput input;
    private PlayerStamina stamina;

    [Header("Movement")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 10f;
    public float rotationSpeed = 10f;
    public float airControl = 0.5f;

    [Header("Jump & Gravity")]
    public float jumpForce = 8f;
    public float gravity = -20f;
    public float groundDistance = 0.3f;
    public int maxJumps = 2;

    private Vector3 velocity;
    private Vector3 moveDirection;
    private bool isGrounded;
    private int jumpCount;
    private bool isSprinting;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        stamina = GetComponent<PlayerStamina>();
    }

    void Update()
    {
        CheckGround();
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
        Vector2 moveInput = input.MoveInput;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 inputDirection = forward * moveInput.y + right * moveInput.x;
        inputDirection = Vector3.ClampMagnitude(inputDirection, 1f);

        // Sprint logic (delegated decision)
        isSprinting = input.SprintHeld && stamina.CanSprint() && isGrounded && inputDirection.magnitude > 0.1f;

        stamina.UseStamina(isSprinting);

        float speed = isSprinting ? sprintSpeed : walkSpeed;

        if (isGrounded)
        {
            moveDirection = inputDirection * speed;
        }
        else
        {
            Vector3 airMove = inputDirection * speed * airControl;
            moveDirection = Vector3.Lerp(moveDirection, airMove, Time.deltaTime);
        }

        // Rotation
        if (inputDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Jump
        if (input.JumpPressed && jumpCount < maxJumps)
        {
            velocity.y = jumpForce;
            jumpCount++;
        }
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