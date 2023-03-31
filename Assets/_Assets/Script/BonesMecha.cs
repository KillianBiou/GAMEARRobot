using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesMecha : MonoBehaviour
{
    public MechaManager manager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("pfeokakofeopkopkzef");
        manager.FocusOnPart(this);
    }
}
