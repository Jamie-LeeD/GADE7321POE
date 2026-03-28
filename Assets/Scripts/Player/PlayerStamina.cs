using UnityEngine;

public class PlayerStamina : MonoBehaviour, ISimpleListener
{
    public float maxStamina = 5f;
    public float currentStamina;

    public float drainRate = 1.5f;
    public float regenRate = 1f;
    public float regenDelay = 1f;

    private bool isSprinting;
    private float lastSprintTime;

    void Start()
    {
        SimpleEventBus.Instance.AddListener(GameEventType.SprintStart, this);
        SimpleEventBus.Instance.AddListener(GameEventType.SprintStop, this);
        currentStamina = maxStamina;
        SimpleEventBus.Instance.PostNotification(
        GameEventType.StaminaChanged,
        this,
        currentStamina / maxStamina
    );
    }

    

    void Update()
    {
        HandleStamina();
    }

    public void OnEvent(GameEventType eventType, object sender, object param = null)
    {
        if (eventType == GameEventType.SprintStart)
        {
            isSprinting = true;
        }
        else if (eventType == GameEventType.SprintStop)
        {
            isSprinting = false;
            lastSprintTime = Time.time;
        }
    }

    void HandleStamina()
    {
        if (isSprinting && currentStamina > 0f)
        {
            currentStamina -= drainRate * Time.deltaTime;
        }
        else
        {
            if (Time.time > lastSprintTime + regenDelay)
            {
                currentStamina += regenRate * Time.deltaTime;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        // Notify others (UI later)
        SimpleEventBus.Instance.PostNotification(
            GameEventType.StaminaChanged,
            this,
            currentStamina / maxStamina
        );
    }

    void OnDestroy()
    {
        if (SimpleEventBus.Instance != null)
        {
            SimpleEventBus.Instance.RemoveListener(GameEventType.SprintStart, this);
            SimpleEventBus.Instance.RemoveListener(GameEventType.SprintStop, this);
        }
    }
}