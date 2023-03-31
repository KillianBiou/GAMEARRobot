using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandTracker : MonoBehaviour
{

    public enum HandSide
    {
        left,
        right
    };

    [System.Serializable]
    public struct attachment{
        public GameObject obj2Attach;
        public HandFinger finger;
    }

    [SerializeField]
    HandSide _handSide = HandSide.left;
    [SerializeField]
    float _boneSize = 0.02f;
    [SerializeField]
    List<attachment> _attachments = new List<attachment>();

    List<InputDevice> _handDevices = new List<InputDevice>(); 
    InputDevice _handDevice;
 
    bool _isTracked = false;
    Hand _handData;
    Bone _rootBone;
    List<Bone>[] _fingerBones = new List<Bone>[5];
    List<GameObject>[] _fingerBoneObjects = new List<GameObject>[5];

    // Start is called before the first frame update
    void OnEnable()
    {
        _handDevice = default;
        for (int i = 0; i < 5; i++)
        {
            _fingerBones[i] = new List<Bone>();
            _fingerBoneObjects[i] = new List<GameObject>();
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < _fingerBones[i].Count; j++)
            {
                Destroy(_fingerBoneObjects[i][j]);
            }
            _fingerBoneObjects[i].Clear();
        }
    }
    void AttachObjects()
    {
        foreach(attachment a in _attachments)
        {
            int fingerID = (int)a.finger;
            if (a.obj2Attach == null || _fingerBoneObjects[fingerID].Count==0)
                continue;

            a.obj2Attach.transform.SetParent(_fingerBoneObjects[fingerID][_fingerBoneObjects[fingerID].Count - 1].transform);
            a.obj2Attach.transform.localPosition = Vector3.zero;
            a.obj2Attach.transform.localRotation = Quaternion.identity;
        }
    }


    void CreateBones(HandFinger fingerID)
    {
        int id = (int)fingerID;
        if (_fingerBoneObjects[id].Count != 0)
            return;

        for (int j = 0; j < _fingerBones[id].Count; j++)
        {
            _fingerBoneObjects[id].Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            if(j==0)
                _fingerBoneObjects[id][j].transform.SetParent(this.transform);
           else
                _fingerBoneObjects[id][j].transform.SetParent(_fingerBoneObjects[id][j-1].transform);

            _fingerBoneObjects[id][j].name = fingerID.ToString() + "_" + j;

            _fingerBoneObjects[id][j].transform.localPosition = Vector3.zero;
            _fingerBoneObjects[id][j].transform.localRotation= Quaternion.identity;
            if(j==0)
                _fingerBoneObjects[id][j].transform.localScale = Vector3.one * _boneSize;
            else
                _fingerBoneObjects[id][j].transform.localScale = Vector3.one;
                
            _fingerBoneObjects[id][j].GetComponent<Renderer>().material.color = Color.HSVToRGB(id / 6.0f, 1.0f, 0.5f + 0.5f * j / (float)_fingerBones[id].Count);
            
        }

        AttachObjects();
    }
    void UpdateRootBone()
    {
        Vector3 pos;
        Quaternion rot;
        if(_rootBone.TryGetPosition(out pos))
            this.transform.position = pos;
        if(_rootBone.TryGetRotation(out rot))
            this.transform.rotation = rot;
    }

    void UpdateBones(HandFinger fingerID)
    {
        CreateBones(fingerID);

        Vector3 position;
        Quaternion rotation;

        int id = (int)fingerID;
        for (int j = 0; j < _fingerBones[id].Count; j++)
        {
            if(_fingerBones[id][j].TryGetPosition(out position))
                _fingerBoneObjects[id][j].transform.position = position;
            if(_fingerBones[id][j].TryGetRotation(out rotation))
                _fingerBoneObjects[id][j].transform.rotation = rotation;

            if(j==0)
                _fingerBoneObjects[id][j].transform.localScale = Vector3.one * _boneSize;
        }
    }

    void ShowHand(bool value=true)
    {
        for (int i = 0; i < 5; i++)
            for (int k = 0; k < _fingerBoneObjects[i].Count; k++)
                _fingerBoneObjects[i][k].GetComponent<Renderer>().enabled = value ;
    }

    void UpdateHand()
    {
        ShowHand(_isTracked);

        if (_isTracked)
        {
            if (_handData.TryGetRootBone(out _rootBone))
                UpdateRootBone();

            for (int i = 0; i < _fingerBones.Length; i++)
            {
                if (_handData.TryGetFingerBones((HandFinger)i, _fingerBones[i]))
                    UpdateBones((HandFinger)i);
            }
        }
 
       
    }

    bool UpdateDevice()
    {
        InputDeviceCharacteristics inputDeviceCharacteristics = InputDeviceCharacteristics.HandTracking;
        // set the characteristic regarding the hand side
        switch (_handSide)
        {
            case HandSide.left:
                inputDeviceCharacteristics = InputDeviceCharacteristics.Left;
                break;
            case HandSide.right:
                inputDeviceCharacteristics = InputDeviceCharacteristics.Right;
                break;
        }

        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, _handDevices);

        _handDevice = default;
        foreach (InputDevice device in _handDevices)
        {
            if (device.TryGetFeatureValue(CommonUsages.isTracked, out _isTracked)
                && _isTracked)
            {
                if (device.TryGetFeatureValue(CommonUsages.handData, out _handData))
                {
                    _handDevice = device;
                    break;
                }
            }
        }



        return (_handDevice != default);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(UpdateDevice())
            UpdateHand();
    }
}
