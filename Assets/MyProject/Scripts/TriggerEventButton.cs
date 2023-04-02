using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TriggerEventButton : TriggerEvent
{


    protected override bool Enter()
    {
        if(!base.Enter())
            return false;
        Button b = GetComponent<Button>();
        b.onClick.Invoke();

        return true;
    }



}
