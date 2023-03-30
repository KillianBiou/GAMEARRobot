using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public enum Language
{
    FRENCH,
    ENGLISH,
    JAPANESE
};

[CreateAssetMenu(fileName = "LanguageManager", menuName = "ScriptableObjects/LanguageManager", order = 1)]
public class LanguageManager : ScriptableObject
{
    public Language language;

    public List<Language> languageList;
    public List<LanguageSO> languageSOList;

    private Dictionary<Language, LanguageSO> languageMap = new Dictionary<Language, LanguageSO>();

    private void Awake()
    {
        languageMap = languageList.Zip(languageSOList, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
    }

    public string GetText(DialogueID imageID)
    {
        return languageMap[language].GetDialogue(imageID);
    }

    public void FrenchLanguage()
    {
        language = global::Language.FRENCH;
        InstanciatorManager.instance.RefreshText(language);
    }

    public void EnglishLanguage()
    {
        language = global::Language.ENGLISH;
        InstanciatorManager.instance.RefreshText(language);
    }

    public void JapaneseLanguage()
    {
        language = global::Language.JAPANESE;
        InstanciatorManager.instance.RefreshText(language);
    }
}
