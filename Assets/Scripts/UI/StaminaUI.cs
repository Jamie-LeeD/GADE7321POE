using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour, ISimpleListener
{
    //public Image staminaFill;
    public Slider staminaSlider;

    void Start()
    {
        SimpleEventBus.Instance.AddListener(GameEventType.StaminaChanged, this);
    }

    void OnDestroy()
    {
        if (SimpleEventBus.Instance != null)
        {
            SimpleEventBus.Instance.RemoveListener(GameEventType.StaminaChanged, this);
        }
    }

    public void OnEvent(GameEventType eventType, object sender, object param = null)
    {
        if (eventType == GameEventType.StaminaChanged)
        {
            float percent = (float)param; // value between 0–1
            //staminaFill.fillAmount = percent;
            staminaSlider.value = percent;
        }
    }
}