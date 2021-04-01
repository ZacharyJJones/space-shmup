using UnityEngine;

/// <summary> Class for allowing entities to be trackable via EntityManager. </summary>
public class Entity : MonoBehaviour
{
    public EntityType Type;

    // hidden because it does not matter what you set it as here. It gets overridden when added to entity manager.
    [HideInInspector]
    public int ID;


    void Start()
    {
        EntityManager.Instance?.Add(this, Type);
        transform.SetParent(EntityManager.Instance.transform, true);
    }

    /// <summary> Removes this entity from being tracked by the entity manager, so it may be destroyed safely. </summary>
    public void RemoveSelf() => EntityManager.Instance.Remove(Type, ID);
}
