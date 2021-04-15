﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.GetComponent<T>();
            }
        }
        return null;
    }
    public static void GetAllChildren(Transform parent, ref List<Transform> transforms)
    {

        foreach (Transform t in parent)
        {

            transforms.Add(t);

            GetAllChildren(t, ref transforms);

        }

    }
}
