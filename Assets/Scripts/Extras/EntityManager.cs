using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    // Singleton Pattern props
    public static EntityManager Instance;
    public static bool InstanceExists => (Instance != null);
    

    // vars
    public Bounds ActiveZone;
    public Dictionary<EntityType, List<EntityManagerItem>> Entities;
    private Dictionary<EntityType, int> _nextIDToGenerateByEntityType;


    // props
    public GameObject ActivePlayerEntity => GetRandom(EntityType.Player);


    // methods
    void Awake()
    {
        if (InstanceExists)
        {
            Destroy(this);
            return;
        }

        Entities = new Dictionary<EntityType, List<EntityManagerItem>> { };
        _nextIDToGenerateByEntityType = new Dictionary<EntityType, int> { };
        Instance = this;
    }


    void Update()
    {
        // might be nice to not run this every frame.
        _entitiesCleanup();
    }

    private void _entitiesCleanup()
    {
        var keys = Entities.Keys;
        foreach (EntityType key in keys)
        {
            // going backwards through list for simpler removal
            var list = Entities[key];
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (ActiveZone.Contains(list[i].GameObject.transform.position))
                {
                    continue;
                }

                // should not interfere with list, as the gameobject being destroyed is just a property of the list item
                Destroy(list[i].GameObject);
                Entities[key].RemoveAt(i);
            }
        }
    }


    public GameObject GetRandom(EntityType type)
    {
        if (Entities.TryGetValue(type, out var data) && data.Count > 0)
        {
            int i = Random.Range(0, data.Count);
            return data[i].GameObject;
        }
        else
        {
            return null;
        }
    }


    public void Add(Entity entity, EntityType type)
    {
        // generate id for entity
        if (!_nextIDToGenerateByEntityType.ContainsKey(type))
        {
            _nextIDToGenerateByEntityType.Add(type, 0);
        }

        entity.ID = _nextIDToGenerateByEntityType[type];
        _nextIDToGenerateByEntityType[type]++;


        if (!Entities.ContainsKey(type))
        {
            Entities.Add(type, new List<EntityManagerItem> { });
        }
            

        Entities[type].Add(new EntityManagerItem(entity.ID, entity.gameObject));
    }

    public void Remove(EntityType type, int id)
    {
        if (!Entities.TryGetValue(type, out var list))
        {
            return;
        }

        int index = list.FindIndex(x => x.ID == id);
        list.RemoveAt(index);
    }
}

[System.Serializable]
public struct EntityManagerItem : System.IEquatable<EntityManagerItem>
{
    public int ID;
    public GameObject GameObject;


    public EntityManagerItem(int id, GameObject gameObject)
    {
        ID = id;
        GameObject = gameObject;
    }
    

    public bool Equals(EntityManagerItem other) => (ID == other.ID);
}
