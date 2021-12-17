using System;
using UnityEngine;

/// <summary> Class for allowing entities to be tracked via EntityManager. </summary>
public abstract class Entity : MonoBehaviour
{
    public virtual EntityType Type => EntityType.Undefined;


    protected virtual void Start()
    {
        if (!EntityManager.Instance)
            return;

        EntityManager.Instance.AddEntity(Type, this);
    }

    /// <summary> Removes this entity from being tracked by the entity manager, so it may be destroyed safely. </summary>
    public void RemoveSelf()
    {
        EntityManager.Instance.RemoveEntity(Type, this);
    }
}
