using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class InstanciatorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject prefabMecha;
    public LanguageManager languageManager;

    public static InstanciatorManager instance;
    private ARTrackedImageManager imageManager;
    private Dictionary<DialogueID, List<GameObject>> instanciatedTrackedImage = new Dictionary<DialogueID, List<GameObject>>();

    void OnEnable() => imageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => imageManager.trackedImagesChanged -= OnChanged;


    private void Awake()
    {
        instance = this;
        imageManager = GetComponent<ARTrackedImageManager>();
    }

    public void Register(PartBuilder partBuilder)
    {
        if (!instanciatedTrackedImage.ContainsKey(partBuilder.dialogueID))
        {
            instanciatedTrackedImage.Add(partBuilder.dialogueID, new List<GameObject>());
        }
        instanciatedTrackedImage[partBuilder.dialogueID].Add(partBuilder.gameObject);

        partBuilder.RefreshText();
    }

    public void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        
        foreach (var newImage in eventArgs.added)
        {
            string text = languageManager.GetText(LanguageSO.String2DialogueID(newImage.referenceImage.name));
            
            if(newImage.referenceImage.name == "Mecha")
            {
                GameObject temp = Instantiate(prefabMecha, newImage.transform);
                if(temp.GetComponent<PartBuilder>())
                    temp.GetComponent<PartBuilder>().Initialize(LanguageSO.String2DialogueID("Title" + newImage.referenceImage.name), LanguageSO.String2DialogueID(newImage.referenceImage.name));
            }
            else
            {
                GameObject temp = Instantiate(prefab, newImage.transform);
                if (temp.GetComponent<PartBuilder>())
                    temp.GetComponent<PartBuilder>().Initialize(LanguageSO.String2DialogueID("Title" + newImage.referenceImage.name), LanguageSO.String2DialogueID(newImage.referenceImage.name));
            }
        }

        foreach (var updatedImage in eventArgs.updated)
        {
        }

        foreach (var removedImage in eventArgs.removed)
        {
        }
    }

    public void RefreshText(Language language)
    {
        foreach(DialogueID key in instanciatedTrackedImage.Keys)
        {
            foreach(GameObject gameObject in instanciatedTrackedImage[key])
            {
                gameObject.GetComponent<PartBuilder>().RefreshText();
            }
        }
    }
}


// trackables
