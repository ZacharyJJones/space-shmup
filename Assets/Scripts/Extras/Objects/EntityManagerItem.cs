using UnityEngine;
using System;

[Serializable]
public struct EntityManagerItem : IEquatable<EntityManagerItem>
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
