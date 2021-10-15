using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    // Editor Fields
    public AvailableModsScriptable Mods;

    // Runtime Fields
    public int WaveNumber;
    public int EnemiesKilledThisWave;
    private List<Mod> PlayerMods;
    private List<Mod> EnemyMods;

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

        PlayerMods = new List<Mod> { };
        EnemyMods = new List<Mod> { };
    }

    public GameObject GetRandomEnemyFromThisWave()
    {
        return null;

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
        EnemiesKilledThisWave++;
        if (EnemiesKilledThisWave > 5  /*WaveKillThrehold*/)
        {
            // stop spawning enemies / make sure they all disappear / move them offscreen
            // spawn upgrade options (bouncing text on screen: COLLECT AN UPGRADE)
        }
    }
}
