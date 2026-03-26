using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : MonoBehaviour, ISimpleListener
{
    [Header("References")]
    public ThirdPersonCamera cameraController;
    public Transform cameraTransform;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;

    [Header("Movement")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 10f;
    public float rotationSpeed = 10f;
    public float airControl = 0.5f;
    public bool canMove = true;

    [Header("Jump & Gravity")]
    public float jumpForce = 8f;
    public float gravity = -20f;
    public float groundDistance = 0.3f;

    [HideInInspector] public CharacterController controller;
    [HideInInspector] public PlayerInput input;

    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public bool isGrounded;

    private PlayerState currentState;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        //animator = GetComponent<Animator>();

        SimpleEventBus.Instance.AddListener(GameEventType.DialogueStart, this);
        SimpleEventBus.Instance.AddListener(GameEventType.DialogueEnd, this);

        ChangeState(new IdleState(this));
    }

    void Update()
    {
        CheckGround();

        currentState.HandleInput();
        currentState.Update();

        ApplyGravity();
        Move();

        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetBool("IsGrounded", isGrounded);
    }

    public void ChangeState(PlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
    }

    void Move()
    {
        if (!canMove)
        {
            Debug.Log("Movement BLOCKED");
            return;
        }

        Debug.Log("Movement ACTIVE");

        controller.Move((moveDirection + velocity) * Time.deltaTime);
    }
    public Vector3 GetCameraRelativeInput()
    {
        Vector2 moveInput = input.MoveInput;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * moveInput.y + right * moveInput.x;
        return Vector3.ClampMagnitude(direction, 1f);
    }

    public void OnEvent(GameEventType eventType, object sender, object param = null)
    {
        if (eventType == GameEventType.DialogueStart)
        {
            ChangeState(new DialogueLockState(this));
        }
        else if (eventType == GameEventType.DialogueEnd)
        {
            ChangeState(new IdleState(this));
        }
    }
    void OnEnable()
    {
        if (SimpleEventBus.Instance == null) return;

        SimpleEventBus.Instance.AddListener(GameEventType.DialogueStart, this);
        SimpleEventBus.Instance.AddListener(GameEventType.DialogueEnd, this);
    }

    void OnDisable()
    {
        if (SimpleEventBus.Instance == null) return;

        SimpleEventBus.Instance.RemoveListener(GameEventType.DialogueStart, this);
        SimpleEventBus.Instance.RemoveListener(GameEventType.DialogueEnd, this);
    }

}