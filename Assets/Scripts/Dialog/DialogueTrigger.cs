using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string dialogueFileName;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            DialogueManager manager = FindFirstObjectByType<DialogueManager>();
            
            manager.StartDialogue(dialogueFileName);


            Debug.Log("DialogueStart EVENT SENT");
            SimpleEventBus.Instance.PostNotification(GameEventType.DialogueStart, this);
            triggered = true;
        }
    }
}