using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FovEvent : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f,90.0f)]
    float _fovAngle = 45.0f;
    [SerializeField]
    bool _detectMainCameraInFov = true;

    [SerializeField]
    GameObject _objectToDetectInFov = null;

    [SerializeField]
    UnityEvent _enter;
    [SerializeField]
    UnityEvent _exit;

    private MechaManager _mechaManager;
    bool _inCone = false;

    private void Start()
    {
        if (_detectMainCameraInFov)
            _objectToDetectInFov = Camera.main.gameObject;
        _mechaManager = GetComponentInParent<BonesMecha>().manager;
    }

    private void Enter()
    {
        _enter.Invoke();
    }

    private void Exit()
    {
        _exit.Invoke();
    }

    public void Reset()
    {
        _inCone = false;
    }

    private void Update()
    {
        if(_mechaManager.State != ViewState.FOCUS || _mechaManager.GetCurrentFocus() != GetComponentInParent<BonesMecha>())
        {
            return;
        }

        Vector3 posInMe = this.transform.InverseTransformPoint(_objectToDetectInFov.transform.position);

        bool inConeTemp = false;
        if (posInMe.z < 0)
        {
            //dehors (derriere)
            inConeTemp = false;
        }
        else
        {
            posInMe.Normalize();
            if (posInMe.z > Mathf.Cos(_fovAngle * Mathf.PI / 180.0f))
                inConeTemp = true; // dans le cone
            else
                inConeTemp = false; // en dehors du cone
        }

        if (inConeTemp != _inCone)
        {
            if (inConeTemp)
                Enter();
            else
                Exit();

            _inCone = inConeTemp;
        }
    }

}
