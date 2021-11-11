using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Editor Fields
    public BoxCollider2D SpawnZone;
    public float SpawnRate = 25f;
    public float SpawnThreshold = 100f;

    // Runtime Fields
    private float _spawnProgress;

    
    private void Start()
    {
        _spawnProgress = 0;
        GameDataManager.Instance.OnWaveEnd += _onWaveEndEvent;
        GameDataManager.Instance.OnWaveStart += _onWaveStartEvent;
    }

    private void _onWaveEndEvent(object sender)
    {
        enabled = false;
    }
    private void _onWaveStartEvent(object sender)
    {
        _spawnProgress = 0;
        enabled = true;
    }

    private void FixedUpdate()
    {
        _spawnProgress += SpawnRate;
        while (_spawnProgress >= SpawnThreshold)
        {
            _spawnEnemy();
            _spawnProgress -= SpawnThreshold;
        }
    }

    private void _spawnEnemy()
    {
        var spawnPos = Utils.GetRandomPointInCollider(SpawnZone);
        var chosenEnemy = GameDataManager.Instance.GetRandomEnemyFromWave();
        Instantiate(chosenEnemy, spawnPos, Quaternion.Euler(0, 0, -180));
    }
}
