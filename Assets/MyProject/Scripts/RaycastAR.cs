using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Camera))]
public class RaycastAR : MonoBehaviour
{
    Camera _camera;
    [SerializeField]
    GameObject _prefab;
    [SerializeField]
    GameObject _reticle;

    bool _collision = false;
    RaycastHit _hit;
    void InstantiatePrefab()
    {
        if (_prefab == null || !_collision)
        {
            Debug.LogError("No prefab or no collision : cannot instantiate !");
            return;
        }

        GameObject go = Instantiate(_prefab);
        go.name = _prefab.name;
        go.transform.position = _hit.point;
        go.AddComponent<ARAnchor>();
        Debug.Log("Prefab instanciated : " + go.name);
    }

    void UpdateReticle()
    {
        if (_collision)
        {
            if (_reticle != null)
            {
                _reticle.SetActive(true);
                _reticle.transform.position = _hit.point;
            }
        }
        else
        {
            _reticle.SetActive(false);
        }
    }
  

    void Update()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
       _collision = Physics.Raycast(ray, out _hit, 5.0f);

        UpdateReticle();

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began
            || Input.GetMouseButtonDown(0) 
            || Input.GetKeyDown(KeyCode.Space)  )
        {
             InstantiatePrefab();
        }
    }

}
