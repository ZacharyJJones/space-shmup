using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityManager : MonoBehaviour
{
    // Editor Fields
    public static EntityManager Instance;

    public BoxCollider2D ActiveZone;

    // Runtime Fields
    private Dictionary<EntityType, List<Entity>> Entities;

    public Entity ActivePlayerEntity => GetRandom(EntityType.Player);


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        EntityType[] entityTypeValues = Enum.GetValues(typeof(EntityType))
            .Cast<EntityType>().Except(new[] {EntityType.Undefined}).ToArray();

        Entities = entityTypeValues.ToDictionary(
            x => x,
            x => new List<Entity>()
        );
    }

    private void FixedUpdate()
    {
        _entitiesCleanup();
    }

    private void _entitiesCleanup()
    {
        foreach (var list in Entities.Values)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (!Utils.IsPointInCollider(list[i].transform.position, ActiveZone))
                {
                    Destroy(list[i].gameObject);
                    list.RemoveAt(i);
                }
            }
        }
    }

    public Entity GetRandom(EntityType type)
    {
        if (!Entities.TryGetValue(type, out var list) || list.Count == 0)
            return null;

        int i = Random.Range(0, list.Count);
        return Entities[type][i];
    }

    public void AddEntity(EntityType type, Entity entity)
    {
        if (!Entities.ContainsKey(type))
            return;

        Entities[type].Add(entity);

        // to avoid hierarchy clutter
        entity.transform.SetParent(Instance.transform, true);
    }

    public void RemoveEntity(EntityType type, Entity entity)
    {
        if (!Entities.ContainsKey(type))
            return;

        Entities[type].Remove(entity);
    }
}
