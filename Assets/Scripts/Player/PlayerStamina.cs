using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 5f;
    public float currentStamina { get; private set; }

    public float drainRate = 1.5f;
    public float regenRate = 1f;
    public float regenDelay = 1f;

    private float lastSprintTime;

    void Start()
    {
        currentStamina = maxStamina;
    }

    public void UseStamina(bool isSprinting)
    {
        if (isSprinting && currentStamina > 0f)
        {
            currentStamina -= drainRate * Time.deltaTime;
            lastSprintTime = Time.time;
        }
        else
        {
            if (Time.time > lastSprintTime + regenDelay)
            {
                currentStamina += regenRate * Time.deltaTime;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }

    public bool CanSprint()
    {
        return currentStamina > 0.1f;
    }
}