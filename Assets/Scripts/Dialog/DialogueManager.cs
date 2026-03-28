using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject Dialogue;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI messageText;
    public Image iconImage;

    private MyQueue<DialogueItem> dialogueQueue = new MyQueue<DialogueItem>();

    public void StartDialogue(string dialogueFileName)
    {
        Dialogue.SetActive(true);
        LoadDialogue(dialogueFileName);
        DisplayNextDialogue();
    }


    void LoadDialogue(string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Dialogue/" + fileName);

        if (jsonFile == null)
        {
            Debug.LogError("Dialogue file not found: " + fileName);
            return;
        }

        DialogueContainer container =
            JsonUtility.FromJson<DialogueContainer>(jsonFile.text);

        foreach (DialogueItem item in container.dialogues)
        {
            dialogueQueue.Enqueue(item);
        }
    }


    public void DisplayNextDialogue()
    {
        if (dialogueQueue.IsEmpty())
        {
            EndDialogue();
            return;
        }

        DialogueItem item = dialogueQueue.Dequeue();

        titleText.text = item.title;
        messageText.text = item.message;

        Sprite icon = Resources.Load<Sprite>("Icons/" + item.icon);

        if (icon != null)
        {
            iconImage.sprite = icon;
        }
    }


    void EndDialogue()
    {
        SimpleEventBus.Instance.PostNotification(GameEventType.DialogueEnd, this);
        Dialogue.SetActive(false);
    }
}