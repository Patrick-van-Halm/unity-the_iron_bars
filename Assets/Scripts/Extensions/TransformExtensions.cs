using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        static public void DeleteChildren(this Transform t)
        {
            for(int i = 0; i < t.childCount; i++)
            {
                Object.Destroy(t.GetChild(i).gameObject);
            }
        }
    }
}