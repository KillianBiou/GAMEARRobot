using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public enum interactionType
    {
        click,
        timer
    };

    [SerializeField]
    interactionType _type = interactionType.click;
    [SerializeField]
    [Range(0.0f,30.0f)]
    float _timer = 5.0f;

    [SerializeField]
    UnityEvent _eventEnter;
    [SerializeField]
    UnityEvent _eventExit;
    [SerializeField]
    UnityEvent _event;

    float _enterTime = -1.0f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float _progression = 0.0f;

    public void Enter()
    {
        if(_type==interactionType.timer)
            StartCoroutine(EventAfterTimer());

        _enterTime = Time.time;

        _eventEnter.Invoke();
    }

    public void Exit()
    {
        StopAllCoroutines();
        _enterTime = -1.0f;

        _eventExit.Invoke();
    }

    public float getElapsedTime()
    {
        if (_enterTime < 0)
            return 0.0f;

        return (Time.time - _enterTime);
    }

    public float getProgression()
    {
       
        if (_type == interactionType.click || _timer == 0.0f)
            _progression = 1.0f;
        
       else
            _progression =  Mathf.Clamp01(getElapsedTime()/_timer);

        return _progression;
    }

    public void Click()
    {
        if (_type != interactionType.click)
            return;

        _event.Invoke();
    }

    IEnumerator EventAfterTimer()
    {
        yield return new WaitForSeconds(_timer);
        _event.Invoke();
    }
}
