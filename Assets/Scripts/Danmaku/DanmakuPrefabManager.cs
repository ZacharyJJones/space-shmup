using System.Collections.Generic;
using UnityEngine;
using DanmakU;
using System.Linq;

public class DanmakuPrefabManager : MonoBehaviour
{
    // Editor Fields
    // This should probably be moved out to a scriptable object, so the list will be shared between any danmaku prefab managers.
    public DanmakuPrefabInfo[] Prefabs;

    // Runtime Fields
    public static DanmakuPrefabManager Instance;
    private Dictionary<string, DanmakuPrefab> _prefabDict;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        // set up as singleton
        Instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(this.gameObject);

        Instance = this;
        _prefabDict = Prefabs.ToDictionary(
            x => x.Name,
            x => x.Prefab
        );
    }

    public DanmakuPrefab GetPrefab(string name)
    {
        return _prefabDict[name];
    }
}
