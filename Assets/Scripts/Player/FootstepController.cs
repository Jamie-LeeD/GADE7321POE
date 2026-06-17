using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public enum FootstepMode
    {
        Off,
        Walk,
        Sprint
    }
    public float walkInterval = 0.45f;
    public float sprintInterval = 0.28f;

    private PlayerStateMachine stateMachine;
    private FootstepMode mode = FootstepMode.Off;
    private float stepTimer;

    void Awake()
    {
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    public void SetMode(FootstepMode newMode)
    {
        mode = newMode;
        stepTimer = 0f;
    }

    void Update()
    {
        if (mode == FootstepMode.Off || stateMachine == null)
        {
            return;
        }

        // Footsteps only play while grounded. Airborne states disable the mode first.
        if (!stateMachine.isGrounded)
        {
            return;
        }

        stepTimer += Time.deltaTime;
        float interval = mode == FootstepMode.Sprint ? sprintInterval : walkInterval;

        if (stepTimer < interval)
        {
            return;
        }

        stepTimer = 0f;

        if (SfxManager.Instance == null)
        {
            return;
        }

        string key = mode == FootstepMode.Sprint ? SfxKeys.FootstepSprint : SfxKeys.FootstepWalk;
        SfxManager.Instance.PlaySound(key);
    }
}
