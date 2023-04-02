using TMPro;
using UnityEngine;

public class PartBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject titleObject;
    [SerializeField]
    private GameObject textObject;

    [SerializeField]
    private bool AutoInitialize;
    [SerializeField]
    private bool trackCamera;

    public DialogueID titleID;
    public DialogueID dialogueID;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        if (AutoInitialize)
            InstanciatorManager.instance.Register(this);
    }

    public void Initialize(DialogueID titleID, DialogueID dialogueID)
    {
        this.dialogueID = dialogueID;
        this.titleID = titleID;
        Debug.Log("DEBUG CODE : " + dialogueID.ToString());
        Debug.Log("DEBUG CODE : " + titleID.ToString());
        RefreshText();
    }

    public void RefreshText()
    {
        titleObject.GetComponent<TextMeshProUGUI>().text = InstanciatorManager.instance.languageManager.GetText(titleID);
        textObject.GetComponent<TextMeshProUGUI>().text = InstanciatorManager.instance.languageManager.GetText(dialogueID);
    }

    public void Setup()
    {
        InstanciatorManager.instance.Register(this);
    }

    private void Update()
    {
        /*if(trackCamera)
            transform.localRotation = Quaternion.LookRotation(camera.transform.position - transform.position, Vector3.up);*/
    }
}
