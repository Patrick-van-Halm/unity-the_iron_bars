using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Railbound.Extensions
{
    public static class RectTransformExtensions
    {
        static public bool ContainsPosition(this RectTransform rt, Vector3 worldPos, bool allowInverse = false)
        {
            return rt.rect.Contains(rt.InverseTransformPoint(worldPos), allowInverse);
        }

    }
}