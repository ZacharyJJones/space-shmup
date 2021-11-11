using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    // Editor Fields
    public AvailableModsScriptable Mods;
    public GameObject EnemyToSpawn;

    // Runtime Fields
    public int WaveNumber;
    public int EnemiesDefeatedThisWave;
    public int EnemyDefeatWaveThreshold;

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
        DontDestroyOnLoad(this.gameObject);
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
    
    // once player selects an option, can start next wave

    public void RegisterEnemyDeath(Enemy enemy)
    {
        EnemiesDefeatedThisWave++;
        if (EnemiesDefeatedThisWave <= EnemyDefeatWaveThreshold)
            return;
        
        OnWaveEnd?.Invoke(this);
        Debug.Log("Wave Ended.");
        
        // What should happen??
        // 1. disable enemy spawner
        // 2. wait until all enemies are gone
        // 3. spawn end-of-level choices
        // -- right now, this means picking a "level" to go to.
            
        // 4. Begin next level
        // -- hack solution for now: just wait some time then start
        StartCoroutine(Utils.SimpleWait(5f, () =>
        {
            OnWaveStart?.Invoke(this);
            Debug.Log("Wave Started.");
        }));
    }
}
