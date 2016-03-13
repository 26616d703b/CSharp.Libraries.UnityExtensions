using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class GameObjectExtensions
{
    public static void AddChild(this GameObject parent, GameObject child)
    {
        if (parent == null)
            throw new NullReferenceException();

        if (child == null)
            throw new ArgumentNullException();

        child.transform.parent = parent.transform;
    }

    public static void AddChildren(this GameObject parent, GameObject[] children)
    {
        if (parent == null)
            throw new NullReferenceException();

        if (children == null)
            throw new ArgumentNullException();

        foreach (var child in children)
        {
            AddChild(parent, child);
        }
    }

    public static int CountChildren(this GameObject parent)
    {
        if (parent == null)
            throw new NullReferenceException();

        return parent.transform.childCount;
    }

    public static void DestroyChildren(this GameObject parent)
    {
        if (parent == null)
            throw new NullReferenceException();

        foreach (Transform child in parent.transform)
        {
            Object.Destroy(child.gameObject);
        }
    }

    public static GameObject FindChild(this GameObject parent, string childName, bool exactMatch = true)
    {
        if (parent == null)
            throw new NullReferenceException();

        if (childName == null)
            throw new ArgumentNullException();

        if (exactMatch)
        {
            foreach (Transform childTransform in parent.transform)
            {
                if (childTransform.name.Equals(childName))
                {
                    return childTransform.gameObject;
                }
            }
        }
        else
        {
            foreach (Transform childTransform in parent.transform)
            {
                if (childTransform.name.Contains(childName))
                {
                    return childTransform.gameObject;
                }
            }
        }

        return null;
    }

    public static GameObject FindDescendant(this GameObject ancestor, string descendantName, bool exactMatch = true)
    {
        if (ancestor == null)
            throw new NullReferenceException();

        if (descendantName == null)
            throw new ArgumentNullException();

        var descendant = ancestor.FindChild(descendantName, exactMatch);

        if (descendant)
        {
            return descendant;
        }
        foreach (Transform currentDescendantTransform in ancestor.transform)
        {
            if (descendant = currentDescendantTransform.gameObject.FindDescendant(descendantName, exactMatch))
            {
                return descendant;
            }
        }

        return null;
    }

    public static bool IsParentOf(this GameObject parent, GameObject child)
    {
        if (parent == null)
            throw new NullReferenceException();

        if (child == null)
            throw new ArgumentNullException();

        return child.transform.IsChildOf(parent.transform);
    }

    public static bool IsAncestorOf(this GameObject ancestor, GameObject descendant)
    {
        if (ancestor == null)
            throw new NullReferenceException();

        if (descendant == null)
            throw new ArgumentNullException();

        if (ancestor.IsParentOf(descendant))
        {
            return true;
        }
        foreach (Transform currentDescendantTransform in ancestor.transform)
        {
            if (ancestor.IsAncestorOf(currentDescendantTransform.gameObject))
            {
                return true;
            }
        }

        return false;
    }
}