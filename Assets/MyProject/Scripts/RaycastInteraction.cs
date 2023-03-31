using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastInteraction : MonoBehaviour
{
    [SerializeField]
    Transform _refRaycast = null;
    [SerializeField]
    GameObject _reticle;

    bool _collision = false;
    RaycastHit _hit;
    [SerializeField]
    GameObject _selectedObject = null;


    void OnEnable()
    {
        if (_refRaycast == null)
            _refRaycast = this.transform;
        Unselect();
    }

    void OnDisable()
    {
        Unselect();
    }

    void UpdateRayCast()
    {
        Ray ray = new Ray(_refRaycast.position, _refRaycast.forward);
        _collision = Physics.Raycast(ray, out _hit, 100.0f);
    }

    void UpdateReticle()
    {
        if (_collision)
        {
            if (_reticle != null)
            {
                _reticle.SetActive(true);
                _reticle.transform.position = _hit.point;
                _reticle.transform.LookAt(_hit.point + _hit.normal, Vector3.up);
                _reticle.transform.localScale = Vector3.one;
                if (_selectedObject != null)
                {
                    float progression = 0.0f;
                    Interaction[] interactions = _selectedObject.GetComponentsInChildren<Interaction>();
                    foreach(Interaction i in interactions)
                    {
                        progression = Mathf.Max(progression, i.getProgression());
                    }
                    _reticle.transform.localScale = Vector3.one * (1.0f - progression);
                }
            }
        }
        else
        {
            _reticle.SetActive(false);
        }
    }

    void Unselect()
    {
        if (_selectedObject != null)
        {
            Interaction[] interactions = _selectedObject.GetComponentsInChildren<Interaction>();
            foreach (Interaction i in interactions)
                i.Exit();
            _selectedObject = null;
        }
    }
  
    void UpdateSelected()
    {
        if (_collision )
        {
            Interaction interaction = _hit.collider.GetComponent<Interaction>();
            if (interaction != null)
            {
                if (_selectedObject != _hit.collider.gameObject)
                {
                    if (_selectedObject != null)
                        Unselect();

                    _selectedObject = _hit.collider.gameObject;

                    Interaction[] interactions = _selectedObject.GetComponentsInChildren<Interaction>();
                    foreach (Interaction i in interactions)
                        i.Enter();
                }
                return;
            }
        }

        Unselect();

    }

    void ClickOnSelected()
    {
        if (_selectedObject == null)
            return;

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began
            || Input.GetMouseButtonDown(0)
            || Input.GetKeyDown(KeyCode.Space))
        {
            Interaction[] interactions = _selectedObject.GetComponentsInChildren<Interaction>();
            foreach (Interaction i in interactions)
                i.Click();
        }
    }

    void Update()
    {
        UpdateRayCast();

        UpdateSelected();
        ClickOnSelected();

        UpdateReticle();
        
    }

}
