using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Editor Fields
    public BoxCollider2D SpawnZone;
    public float BaseSpawnProgressRate = 25f;
    public bool SpawningEnabled = true;

    // Runtime Fields
    // Eventually, _currentSpawnProgressRate could be affected by # of waves, or similar.
    private float _currentSpawnProgressRate => BaseSpawnProgressRate;
    private float _spawnProgress = 0;


    private void FixedUpdate()
    {
        // temporary
        return;

        // Note: Consider for future, whether spawning should be enabled / disabled based on this script's
        // ... existence or enable/disable state itself. No need for this check EVERY FRAME then.
        if (!SpawningEnabled)
            return;

        _spawnProgress += _currentSpawnProgressRate;
        while (_spawnProgress >= 100f)
        {
            _spawnEnemy();
            _spawnProgress -= 100f;
        }
    }

    private void _spawnEnemy()
    {
        var spawnPos = Utils.GetRandomPointInCollider(SpawnZone);

        var chosenEnemy = GameDataManager.Instance.GetRandomEnemyFromThisWave();

        var obj = Instantiate(chosenEnemy);
        obj.transform.position = spawnPos;
    }
}
