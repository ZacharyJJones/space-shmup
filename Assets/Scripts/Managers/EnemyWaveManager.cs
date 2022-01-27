using System;
using System.Collections;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance;

    // Editor Fields
    public BoxCollider2D SpawnZone;
    public GameObject EnemyToSpawn;
    public float SpawnRate = 10f;
    public float SpawnThreshold = 20f;

    public GameObject AreaChoosePrefab;
    public Vector3 AreaPrefabSpawnLocation;

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

    // public for other scripts to trigger a wave start.
    public void StartNextWave(object sender)
    {
        OnWaveStart?.Invoke(sender);
    }

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
        // "credit" cost per enemy would be decent. Then for a given wave could determine if wave is complete yet based
        // ... on "credit value killed" or similar.
        // --> more broadly applicable than just # of enemies
        // --> enemies could also have settings for delay between this one spawning and the next, for more powerful foes

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
        if (!enabled) return;

        Debug.Log("Enemy was defeated");
        EnemiesDefeatedThisWave++;
        if (EnemiesDefeatedThisWave < EnemiesInWave)
            return;

        // 1. Wave Prep & Enemy Spawner Disable
        _nextWaveSetup();
        Debug.Log("Wave Ended.");
        OnWaveEnd?.Invoke(this);


        // 2. wait until all enemies are gone... then
        // 3. spawn end-of-level choices
        StartCoroutine(Utils.SimpleWaitConditional(
            () => (EntityManager.Instance.GetRandom(EntityType.Enemy) == null),
            () => StartCoroutine(Utils.SimpleWait(2f, _spawnEndOfLevelChoices))
        ));
    }

    private void _spawnEndOfLevelChoices()
    {
        string options = "AAEEIIOOUU BCDFGHJKLMNPQRSTVWXYZ 0123456789";
        options = options.Replace(" ", "");
        string[] areasToAssign = new string[3];
        for (int i = 0; i < 3; i++)
        {
            areasToAssign[i] = options[UnityEngine.Random.Range(0, options.Length)].ToString();
        }

        var areaChoicePrefab = Instantiate(
            AreaChoosePrefab,
            AreaPrefabSpawnLocation,
            Quaternion.identity,
            this.transform
        );
        var areaChange = areaChoicePrefab.GetComponent<HoverSelectAreaChange>();
        areaChange.DisplayText = UserInterfaceManager.Instance.AreaDisplayText;
        areaChange.Areas = areasToAssign;
    }

    private void _nextWaveSetup()
    {
        EnemiesDefeatedThisWave = 0;
        WaveNumber++;
        SpawnRate += 2f;
        SpawnThreshold += 1f;
        EnemiesInWave += 3;
    }


    private void _onWaveEndEvent(object sender)
    {
        Debug.Log("Wave Ended");
        enabled = false;
    }
    private void _onWaveStartEvent(object sender)
    {
        Debug.Log("Wave Started");
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
