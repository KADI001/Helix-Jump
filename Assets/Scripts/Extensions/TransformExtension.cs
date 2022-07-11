using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    public static void SetParent(this Transform transform, Transform parent)
    {
        transform.parent = parent;
    }

    public static void SetParent(this Transform transform, GameObject parent)
    {
        transform.parent = parent.transform;
    }

    public static void SetParent(this Transform transform, MonoBehaviour parent)
    {
        transform.parent = parent.gameObject.transform;
    }
}