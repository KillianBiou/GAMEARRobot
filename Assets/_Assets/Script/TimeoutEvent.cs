using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimeoutEvent : MonoBehaviour
{
    [SerializeField]
    private float timeoutTime;

    [SerializeField]
    private UnityEvent timeoutEvent;

    private void Start()
    {
        StartCoroutine(Timeout());
    }

    private IEnumerator Timeout()
    {
        yield return new WaitForSeconds(timeoutTime);
        timeoutEvent.Invoke();
    }
}
