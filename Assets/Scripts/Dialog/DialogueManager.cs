using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // UI References
    
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI messageText;
    public Image iconImage;

    // Name of JSON file inside Resources folder
    public string dialogueFileName;

    // Custom Queue
    private MyQueue<DialogueItem> dialogueQueue = new MyQueue<DialogueItem>();


    void Start()
    {
        LoadDialogue();
        DisplayNextDialogue();
    }


    void LoadDialogue()
    {
        // Load JSON file
        TextAsset jsonFile = Resources.Load<TextAsset>("Dialogue/" + dialogueFileName);

        if (jsonFile == null)
        {
            //Debug.LogError(Resources.Load<TextAsset>(dialogueFileName));
            Debug.LogError("Dialogue file not found!");
            return;
        }

        // Convert JSON to objects
        DialogueContainer container =
            JsonUtility.FromJson<DialogueContainer>(jsonFile.text);

        // Enqueue dialogue items
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

        UpdateUI(item);
    }


    void UpdateUI(DialogueItem item)
    {
        titleText.text = item.title;
        messageText.text = item.message;

        // Load icon from Resources/Icons
        Sprite icon = Resources.Load<Sprite>("Icons/" + item.icon);

        if (icon != null)
        {
            iconImage.sprite = icon;
        }
    }


    void EndDialogue()
    {
        Debug.Log("Dialogue Finished");

        // Optional: hide dialogue UI
        gameObject.SetActive(false);
    }
}