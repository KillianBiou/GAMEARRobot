using TMPro;
using UnityEngine;

public class PartBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject textObject;
    public DialogueID dialogueID;
    
    public void Initialize(DialogueID dialogueID)
    {
        this.dialogueID = dialogueID;
        RefreshText();
    }

    public void RefreshText()
    {
        textObject.GetComponent<TextMeshProUGUI>().text = InstanciatorManager.instance.languageManager.GetText(dialogueID);
    }

    public void Setup()
    {
        InstanciatorManager.instance.Register(this);
    }
}
