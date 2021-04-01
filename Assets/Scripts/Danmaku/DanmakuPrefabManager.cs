using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanmakU;
using System.Linq;

public class DanmakuPrefabManager : MonoBehaviour
{
    // singleton pattern
    public static DanmakuPrefabManager Instance;
    public static bool InstanceExists => (Instance != null);




    public DanmakuPrefabInfo[] Prefabs;


    private Dictionary<string, DanmakuPrefab> _prefabDict;


    void Awake()
    {
        if (InstanceExists)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        _prefabDict = Prefabs.ToDictionary(x => x.Name, x => x.Prefab);
    }

    void OnDestroy()
    {
        // this is only true when this is the instance
        if (_prefabDict != null)
        {
            Instance = null;
        }
    }

    public DanmakuPrefab GetPrefab(string name)
    {
        _prefabDict.TryGetValue(name, out var prefab);
        return prefab;
    }


    [Serializable]
    public struct DanmakuPrefabInfo
    {
        public string Name;
        public DanmakuPrefab Prefab;
    }

}
