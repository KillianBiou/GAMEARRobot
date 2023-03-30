using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class InstanciatorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private LanguageManager languageManager;

    public static InstanciatorManager instance;
    private ARTrackedImageManager imageManager;
    private Dictionary<string, GameObject> instanciatedTrackedImage = new Dictionary<string, GameObject>();

    void OnEnable() => imageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => imageManager.trackedImagesChanged -= OnChanged;


    private void Awake()
    {
        instance = this;
        imageManager = GetComponent<ARTrackedImageManager>();
    }

    public void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        
        foreach (var newImage in eventArgs.added)
        {
            string text = languageManager.GetText(LanguageSO.String2DialogueID(newImage.referenceImage.name));

            GameObject temp = Instantiate(prefab, newImage.transform);
            temp.GetComponent<PartBuilder>().Initialize(text);

            instanciatedTrackedImage.Add(newImage.referenceImage.name, temp);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
        }

        foreach (var removedImage in eventArgs.removed)
        {
            /*if (instanciatedTrackedImage.ContainsKey(removedImage.referenceImage.name))
            {
                Destroy(instanciatedTrackedImage[removedImage.referenceImage.name]);
                instanciatedTrackedImage.Remove(removedImage.referenceImage.name);
            }*/
        }
    }

    public void RefreshText(Language language)
    {
        foreach(string key in instanciatedTrackedImage.Keys)
        {
            instanciatedTrackedImage[key].GetComponent<PartBuilder>().RefreshText(languageManager.GetText(LanguageSO.String2DialogueID(key)));
        }
    }
}


// trackables
