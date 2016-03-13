using System;
using UnityEngine;

public static class ComponentExtensions
{
    public static T GetComponentInChildren<T>(this Component parent, bool includeInactive)
    {
        if (parent == null)
            throw new NullReferenceException();

        var component = default(T);

        if (!includeInactive)
        {
            component = parent.GetComponentInChildren<T>();
        }
        else
        {
            foreach (Transform child in parent.transform)
            {
                component = child.GetComponent<T>();

                if (component != null)
                    break;
            }
        }

        return component;
    }
}