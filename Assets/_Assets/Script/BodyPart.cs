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

    public void Appear()
    {
        GetComponent<Renderer>().enabled = true;
        foreach (Transform t in transform)
        {
            t.GetComponent<Renderer>().enabled = true;
        }
    }

    public void Disapear()
    {
        GetComponent<Renderer>().enabled = false;
        foreach (Transform t in transform)
        {
            t.GetComponent<Renderer>().enabled = false;
        }
    }
}
