using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayUp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Quaternion newLocalRotation = Quaternion.FromToRotation(transform.parent.up, Vector3.up);
        transform.localRotation = newLocalRotation;
    }
}
