using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // Singleton Pattern stuff
    public static GameDataManager Instance;
    public static bool InstanceExists => (Instance != null);





    // this gets assigned by the chooser itself.
   // [HideInInspector] public GameModChooser GameModChooser;


    public int WaveNumber;
    public int EnemiesKilledThisWave;
   // public LevelWavesData WavesData; // this will be calculated later on based on the upgrades posessed by the enemies
   // public LevelWave CurrentWave; // see above

    // when generating wave data, create a set of data pre-calculating the mod effects for each enemy that can spawn.
    // -- this is to avoid tons of unnecessary calculations by doing it all before the wave starts.

    public AvailableModsScriptable Mods;

    public List<Mod> PlayerMods { get; set; }
    public List<Mod> EnemyMods { get; set; }





    public GameObject GetRandomEnemyFromThisWave()
    {
        return null;




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




    void Awake()
    {
        if (InstanceExists)
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




    public void UpdatePlayerStats()
    {
        // apply player mods
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

    /// <summary> Spawns the upgrade objects for the player to collect by flying into. </summary>
    public void SpawnUpgrades()
    {

    }

    public void ProgressToNextWave()
    {
        // assuming this gets called after having collected the upgrade
        // -- re-create the data for 'this wave' based on upgrades etc.


        // load next wave's data
        // set enemies to spawn again
        // reset enemies killed counter
        // make sure mod selection UI is hidden.
    }
}
