using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public BoxCollider2D SpawnZone;

    public float BaseSpawnProgressRate = 25f;

    public bool SpawningEnabled = true;



    private float _spawnProgress = 0;
    public float CurrentSpawnProgressRate { get; set; }

    

    void Update()
    {
        if (!SpawningEnabled)
        {
            return;
        }

        _spawnProgress += CurrentSpawnProgressRate;
        if (_spawnProgress >= 100f)
        {
            SpawnEnemy();
            _spawnProgress = 0;
        }        
    }

    public void SpawnEnemy()
    {
        //var spawnPos = Utils.GetPointInCollider(SpawnZone);

        //var chosenEnemy = GameDataManager.Instance.GetRandomEnemyFromThisWave();

        //var obj = Instantiate(chosenEnemy);
        //obj.transform.position = spawnPos;
    }
}
