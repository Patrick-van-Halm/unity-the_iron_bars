using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

static class ComponentExtensions
{
    public static T[] GetComponentsOnlyInChildren<T>(this Component comp) where T : Component
    {
        return comp.GetComponentsInChildren<T>().Where(c => c.gameObject != comp.gameObject).ToArray();
    }
}