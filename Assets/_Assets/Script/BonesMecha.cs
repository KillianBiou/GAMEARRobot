using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesMecha : MonoBehaviour
{
    public MechaManager manager;

    public List<GameObject> interestPoints;

    public void TriggerFocus()
    {
        manager.FocusOnPart(this);
    }

    public void TriggerReset()
    {
        foreach(GameObject interestPoint in interestPoints)
        {
            interestPoint.transform.GetChild(0).gameObject.SetActive(false);
            interestPoint.GetComponent<FovEvent>().Reset();
        }
    }
}
