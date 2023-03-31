using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewState
{
    GLOBAL,
    FOCUS
}

public class MechaManager : MonoBehaviour
{
    [SerializeField]
    private List<BodyPart> parts;
    [SerializeField]
    private List<BonesMecha> partsBones;

    private ViewState state;

    private void Awake()
    {
        state = ViewState.GLOBAL;
        foreach (var part in parts)
        {
            part.manager = this;
        }
        foreach (var bone in partsBones)
        {
            bone.manager = this;
        }
    }

    public void FocusOnPart(BonesMecha bones)
    {
        int id = partsBones.IndexOf(bones);
        for(int i = 0; i < parts.Count; i++)
        {
            if(i != id)
            {
                parts[i].GetComponent<Renderer>().enabled = false;
            }
        }
        state = ViewState.FOCUS;
    }
}
