using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour
{
    [SerializeField]
    Transform _toFollow = null;

    [Header("POSITION")]
    [SerializeField]
    bool _followPosition = true;
    [SerializeField]
    [Range(0.0f,1.0f)]
    float _positionFilter = 0.1f;

    [Header("ROTATION")]
    [SerializeField]
    bool _followRotation = true;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float _quaternionFilter = 0.1f;

    Vector3 _pos;
    Quaternion _quat;

    void OnEnable()
    {
        if(_toFollow==null)
        {
            _toFollow = this.transform.parent;
            Debug.LogWarning("No transform to follow => I use the parent");
        }

        _pos = _toFollow.position;
        _quat = _toFollow.rotation;
    }

    void Update()
    {
        if (_toFollow == null)
            return;

        if (_followPosition)
        {
            _pos = Vector3.Lerp(_pos, _toFollow.position, _positionFilter);
            this.transform.position = _pos;
        }
        if (_followRotation)
        {
            _quat = Quaternion.Slerp(_quat, _toFollow.rotation, _quaternionFilter);
            this.transform.rotation = _quat;
        }
    }

}
