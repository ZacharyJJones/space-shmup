using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityManager : MonoBehaviour
{
    // Editor Fields
    public static EntityManager Instance;
    public Bounds ActiveZone;

    // Runtime Fields
    private Dictionary<EntityType, List<EntityManagerItem>> Entities;
    private Dictionary<EntityType, int> _nextIDToGenerateByEntityType;

    public GameObject ActivePlayerEntity => GetRandom(EntityType.Player);


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
            x => new List<EntityManagerItem>()
        );
        _nextIDToGenerateByEntityType = entityTypeValues.ToDictionary(
            x => x,
            x => 0
        );
    }

    private void FixedUpdate()
    {
        _entitiesCleanup();
    }

    private void _entitiesCleanup()
    {
        var keys = Entities.Keys;
        foreach (EntityType key in keys)
        {
            var list = Entities[key];
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (ActiveZone.Contains(list[i].GameObject.transform.position))
                {
                    continue;
                }

                Destroy(list[i].GameObject);
                Entities[key].RemoveAt(i);
            }
        }
    }

    public GameObject GetRandom(EntityType type)
    {
        if (!Entities.TryGetValue(type, out var data) || data.Count <= 0)
            return null;

        int i = Random.Range(0, data.Count);
        return data[i].GameObject;
    }

    public void AddEntity(Entity entity, EntityType type)
    {
        if (!Entities.ContainsKey(type))
            return;

        entity.ID = _nextIDToGenerateByEntityType[type];
        Entities[type].Add(new EntityManagerItem(entity.ID, entity.gameObject));
        _nextIDToGenerateByEntityType[type] += 1;
    }

    public void RemoveEntity(EntityType type, int id)
    {
        if (!Entities.TryGetValue(type, out var list))
            return;

        int index = list.FindIndex(x => x.ID == id);
        list.RemoveAt(index);
    }
}
