using UnityEngine;

/// <summary> Class for allowing entities to be tracked via EntityManager. </summary>
public class Entity : MonoBehaviour
{
    // Editor Fields
    public EntityType Type;

    // Runtime Fields
    public int ID { get; set; }


    private void Start()
    {
        if (!EntityManager.Instance)
            return;

        EntityManager.Instance.AddEntity(this, Type);

        // to avoid hierarchy clutter
        transform.SetParent(EntityManager.Instance.transform, true);
    }

    /// <summary> Removes this entity from being tracked by the entity manager, so it may be destroyed safely. </summary>
    public void RemoveSelf() => EntityManager.Instance.RemoveEntity(Type, ID);
}
