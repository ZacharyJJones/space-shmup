using System;
using UnityEditor;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance;

    // Editor Fields
    public BoxCollider2D SpawnZone;
    public GameObject EnemyToSpawn;
    public float SpawnRate = 10f;
    public float SpawnThreshold = 20f;

    // Runtime Fields
    [HideInInspector] public int WaveNumber;
    [HideInInspector] public int EnemiesDefeatedThisWave;
    [HideInInspector] public int EnemiesInWave;
    private float _spawnProgress;

    // Events
    public delegate void WaveEndHandler(object sender);
    public delegate void WaveStartHandler(object sender);
    public event WaveEndHandler OnWaveEnd;
    public event WaveStartHandler OnWaveStart;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        transform.SetParent(null);
    }

    private void Start()
    {
        Instance.OnWaveEnd += _onWaveEndEvent;
        Instance.OnWaveStart += _onWaveStartEvent;

        WaveNumber = 0;
        OnWaveStart.Invoke(this);
    }

    private void FixedUpdate()
    {
        _spawnProgress += SpawnRate * Time.fixedDeltaTime;
        while (_spawnProgress >= SpawnThreshold)
        {
            _spawnEnemy();
            _spawnProgress -= SpawnThreshold;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public GameObject GetRandomEnemyFromWave()
    {
        return EnemyToSpawn;
        // Below code would be to select an enemy to spawn based on certain weights.
        // --> Probably want to change this, it would be cool to have groups/formations/patterns of enemies that spawn
        // ... rather than singular dudes. But that's a problem for another day.

        /* from enemyspawner.cs */


        //var sumOfChances = Enemies.Sum(x => x.Chance);
        //var randomVal = Random.Range(0, sumOfChances - 0.01f);

        //float rollingSum = 0f;
        //GameObject chosenEnemy = null;
        //foreach (LevelEnemy enemy in Enemies)
        //{
        //    if (chosenEnemy != null)
        //        break;

        //    rollingSum += enemy.Chance;
        //    if (rollingSum >= randomVal)
        //    {
        //        chosenEnemy = enemy.Enemy;
        //    }
        //}

        //// pick last enemy in list if none were chosen.
        //if (chosenEnemy == null)
        //{
        //    if (Enemies.Length > 0)
        //        chosenEnemy = Enemies[0].Enemy;
        //    else
        //        return null;
        //}

        //return chosenEnemy;
    }


    public void RegisterEnemyDeath(Enemy enemy)
    {
        Debug.Log("Enemy was defeated");
        EnemiesDefeatedThisWave++;
        if (EnemiesDefeatedThisWave <= EnemiesInWave)
            return;

        EnemiesDefeatedThisWave = 0;
        WaveNumber++;
        SpawnRate += 2f;
        SpawnThreshold += 1f;
        EnemiesInWave += 3;
        Debug.Log("Wave Ended.");
        OnWaveEnd?.Invoke(this);

        // What should happen??
        // 1. disable enemy spawner
        // 2. wait until all enemies are gone
        // 3. spawn end-of-level choices
        // -- right now, this means picking a "level" to go to.

        // 4. Begin next level
        // -- Eventually, will be waiting for player to select an upgrade and pathway before continuing.
        //  -- probably have an event somewhere that says "player picked up an upgrade" and subscribe to it in this script.
        // -- for now, just add a short delay and then start the next one.
        StartCoroutine(Utils.SimpleWait(5f, () =>
        {
            OnWaveStart?.Invoke(this);
            Debug.Log("Wave Started.");
        }));
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
    private void _spawnEnemy()
    {
        var spawnPos = Utils.GetRandomPointInCollider(SpawnZone);
        var chosenEnemy = Instance.GetRandomEnemyFromWave();
        Instantiate(chosenEnemy, spawnPos, Quaternion.Euler(0, 0, -180));
    }
}
