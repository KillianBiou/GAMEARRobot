using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public MechaManager manager;

    [SerializeField]
    private Color fresnelColor;

    private void Awake()
    {
        GetComponent<Renderer>().material.SetColor("_Fresnel_Color", fresnelColor);
        foreach (Transform t in transform)
        {
            t.GetComponent<Renderer>().material.SetColor("_Fresnel_Color", fresnelColor);
        }
    }
}
