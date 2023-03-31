using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EyeTracker : MonoBehaviour
{

    [SerializeField]
    InputActionAsset _xrActions;

    [SerializeField]
    InputActionReference _gazeOK;
    [SerializeField]
    InputActionReference _gazePos;
    [SerializeField]
    InputActionReference _gazeRot;

    bool _tracked = false;
    Vector3 _pos = Vector3.zero;
    Quaternion _rot = Quaternion.identity;

    private void Start()
    {
        _xrActions.Enable();
        _gazeOK.action.Enable();
        _gazePos.action.Enable();
        _gazeRot.action.Enable();
    }

    private void Update()
    {
        _tracked = _gazeOK.action.IsPressed();

        if (_tracked)
        {
            _pos = _gazePos.action.ReadValue<Vector3>();
            _rot = _gazeRot.action.ReadValue<Quaternion>();
        }
        else
        {
            _pos = Camera.main.transform.position;
            _rot = Camera.main.transform.rotation;
        }

        this.transform.position = _pos;
        this.transform.rotation = _rot;
    }

}
