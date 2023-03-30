using TMPro;
using UnityEngine;

public class PartBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject textObject;
    
    public void Initialize(string text)
    {
        textObject.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void RefreshText(string text)
    {
        textObject.GetComponent<TextMeshProUGUI>().text = text;
    }
}
