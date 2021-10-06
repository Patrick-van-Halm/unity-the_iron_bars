using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPasser : MonoBehaviour, IRaycastable3D
{
    public MonoBehaviour passTo;

    public void OnRaycastEnter(RaycastHit hit)
    {
        (passTo as IRaycastable3D)?.OnRaycastEnter(hit);
    }

    public void OnRaycastExit()
    {
        (passTo as IRaycastable3D)?.OnRaycastExit();
    }

    public void OnRaycastStay(RaycastHit hit)
    {
        (passTo as IRaycastable3D)?.OnRaycastStay(hit);
    }
}
