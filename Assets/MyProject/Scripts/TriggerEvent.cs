using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerEvent : MonoBehaviour
{

    [SerializeField]
    protected List<GameObject> _interactors = new List<GameObject>();
    [SerializeField]
    protected UnityEvent _enter;
    [SerializeField]
    protected UnityEvent _exit;
    [SerializeField]
    protected bool _useTimerEvent = false;
    [SerializeField]
    protected UnityEvent _eventTimer;
    [SerializeField]
    [Range(1.0f, 10.0f)]
    protected float _timerPeriod = 1.0f;
    [SerializeField]
    protected bool _repeatTimer = false;

    [SerializeField]
    protected int _nbObjectsOver = 0;

    protected bool _valid = false;
    public bool isValid { get => _valid; set => _valid = value; }

    protected void Start()
    {
        Collider[] cArray = GetComponents<Collider>();
        _valid = false;
        foreach(Collider c in cArray)
        {
            if (!c.isTrigger)
                continue;

            _valid = true;
            break;
        }

        if (!_valid)
        {
            Debug.LogError("TriggerEvent component will not be activate (one collider at least MUST be in trigger mode)");
            this.enabled = false;
        }

        _nbObjectsOver = 0;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (!_interactors.Contains(other.gameObject))
            return;

        _nbObjectsOver = 1;
    }

    protected void OnTriggerStay(Collider other)
    {
        if (!_interactors.Contains(other.gameObject))
            return;

        _nbObjectsOver = 1;
    }


    protected virtual bool Enter()
    {
        if (_nbObjectsOver == 1)
        {
            _enter.Invoke();
            if(_useTimerEvent)
                StartCoroutine(TimerCoroutine());

            return true;
        }
        return false;
    }

    protected virtual bool Exit()
    {
        if (_nbObjectsOver == 0)
        {
            _exit.Invoke();
            StopAllCoroutines();
            return true;
        }
        return false;
    }

    protected virtual bool TriggerTimer()
    {
        _eventTimer.Invoke();
        return true;
    }

    protected IEnumerator TimerCoroutine()
    {
        do
        {
            yield return new WaitForSeconds(_timerPeriod);
            TriggerTimer();
        }
        while (_repeatTimer);
    }


    int _oldNbObjects = 0;
    private void FixedUpdate()
    {
        if (_nbObjectsOver != _oldNbObjects)
        {
            if (_nbObjectsOver == 0)
                Exit();
            else
                Enter();
        }
        _oldNbObjects = _nbObjectsOver;
        _nbObjectsOver = 0;
    }
}
