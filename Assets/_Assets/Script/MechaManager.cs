using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

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

    [SerializeField]
    private GameObject text;

    [SerializeField]
    private float rotationFactor;

    private BonesMecha currentFocus;

    private ARRaycastManager raycastManager;

    public ViewState State { get; private set; }

    private void Awake()
    {
        State = ViewState.GLOBAL;
        raycastManager = GameObject.FindObjectOfType<ARRaycastManager>();
        foreach (var part in parts)
        {
            part.manager = this;
        }
        foreach (var bone in partsBones)
        {
            bone.manager = this;
        }
    }

    private void Update()
    {
        if(State == ViewState.GLOBAL)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                RaycastHit hit;
                if (touch.phase == TouchPhase.Ended){
                    // Drag
                    if(Vector2.Distance(touch.rawPosition, touch.position) >= 20)
                    {
                        Debug.Log("Drag");
                    }
                    // Tap
                    else
                    {
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit))
                        {
                            BonesMecha bonesMecha;
                            if (hit.transform.TryGetComponent<BonesMecha>(out bonesMecha))
                                bonesMecha.TriggerFocus();
                        }
                    }
                }

                // Model Rotation
                if(touch.phase == TouchPhase.Moved)
                {
                    RotateTarget(touch.deltaPosition.x);
                }
            }
        }
        else if(State == ViewState.FOCUS)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                RaycastHit hit;
                if (touch.phase == TouchPhase.Ended)
                {
                    // Drag
                    if (Vector2.Distance(touch.rawPosition, touch.position) >= 20)
                    {
                        Debug.Log("Drag");
                    }
                    // Tap
                    else
                    {
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit))
                        {
                            BonesMecha bonesMecha;
                            if (hit.transform.TryGetComponent<BonesMecha>(out bonesMecha) && bonesMecha == currentFocus)
                                Unfocus();
                        }
                    }
                }

                // Model Rotation
                if (touch.phase == TouchPhase.Moved)
                {
                    RotateTarget(touch.deltaPosition.x);
                }
            }
        }
    }

    public void RotateTarget(float direction)
    {
        switch (State)
        {
            case ViewState.GLOBAL:
                gameObject.transform.GetChild(0).Find("Armature").Rotate(-Vector3.forward * direction * rotationFactor);
                break;
            case ViewState.FOCUS:
                gameObject.transform.GetChild(0).Find("Armature").Rotate(-Vector3.forward * direction * rotationFactor);
                break;
        }
    }

    public void FocusOnPart(BonesMecha bones)
    {
        int id = partsBones.IndexOf(bones);
        for(int i = 0; i < parts.Count; i++)
        {
            if(i != id)
            {
                parts[i].Disapear();
            }
            else
            {
                parts[i].Appear();
            }
        }
        currentFocus = bones;
        text.SetActive(false);
        State = ViewState.FOCUS;
    }

    public void Unfocus()
    {
        int id = partsBones.IndexOf(currentFocus);
        currentFocus.TriggerReset();
        for (int i = 0; i < parts.Count; i++)
        {
            if (i != id)
            {
                parts[i].Appear();
            }
        }
        currentFocus = null;
        text.SetActive(true);
        State = ViewState.GLOBAL;
    }

    public BonesMecha GetCurrentFocus()
    {
        return currentFocus;
    }
}
