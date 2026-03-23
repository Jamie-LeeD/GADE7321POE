using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string dialogueFileName;

    private bool triggered = false;


    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            DialogueManager manager =
                FindFirstObjectByType<DialogueManager>();

            manager.StartDialogue(dialogueFileName);

            triggered = true;
        }
    }
}