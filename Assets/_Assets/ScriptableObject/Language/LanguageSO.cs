using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DialogueID{
    NONE,
    TEST,
    MECHA,
}

[CreateAssetMenu(fileName = "LanguageSO", menuName = "ScriptableObjects/Language")]
public class LanguageSO : ScriptableObject
{
    public List<DialogueID> dialogueID;
    public List<string> dialogueText;

    private Dictionary<DialogueID, string> dialogueDictionary = new Dictionary<DialogueID, string>();

    private void Awake()
    {
        dialogueDictionary = dialogueID.Zip(dialogueText, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
    }

    public string GetDialogue(DialogueID id)
    {
        return dialogueDictionary[id];
    }

    public static DialogueID String2DialogueID(string str)
    {
        switch (str)
        {
            case "Slayer":
                return DialogueID.TEST;
            case "Mecha":
                return DialogueID.MECHA;
        }

        return DialogueID.NONE;
    }
}
