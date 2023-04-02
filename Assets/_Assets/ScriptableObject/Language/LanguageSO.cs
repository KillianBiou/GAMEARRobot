using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DialogueID{
    NONE,
    TITLE_MECHA,
    MECHA,
    TITLE_LATERAL_WING,
    LATERAL_WING,
    TITLE_HANDS,
    HANDS,
    TITLE_THRUSTERS,
    THRUSTERS,
    TITLE_REACTORS,
    REACTORS,
    TITLE_SHIELD,
    SHIELD,
    TITLE_PARTICLEGUN,
    PARTICLEGUN,
    TITLE_SHOTGUN,
    SHOTGUN,
    TITLE_MISSILE,
    MISSILE,
    TITLE_COCKPIT,
    COCKPIT
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
            case "MECHA":
                return DialogueID.MECHA;
            case "TitleMECHA":
                return DialogueID.TITLE_MECHA;

            case "HANDS":
                return DialogueID.HANDS;
            case "TitleHANDS":
                return DialogueID.TITLE_HANDS;

            case "WINGS":
                return DialogueID.LATERAL_WING;
            case "TitleWINGS":
                return DialogueID.TITLE_LATERAL_WING;

            case "REACTORS":
                return DialogueID.REACTORS;
            case "TitleREACTORS":
                return DialogueID.TITLE_REACTORS;

            case "SHIELD":
                return DialogueID.SHIELD;
            case "TitleSHIELD":
                return DialogueID.TITLE_SHIELD;

            case "PARTICLEGUN":
                return DialogueID.PARTICLEGUN;
            case "TitlePARTICLEGUN":
                return DialogueID.TITLE_PARTICLEGUN;

            case "SHOTGUN":
                return DialogueID.SHOTGUN;
            case "TitleSHOTGUN":
                return DialogueID.TITLE_SHOTGUN;

            case "MISSILE":
                return DialogueID.MISSILE;
            case "TitleMISSILE":
                return DialogueID.TITLE_MISSILE;

            case "COCKPIT":
                return DialogueID.COCKPIT;
            case "TitleCOCKPIT":
                return DialogueID.TITLE_COCKPIT;
        }

        return DialogueID.NONE;
    }
}
