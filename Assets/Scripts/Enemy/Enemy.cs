using UnityEngine;
using DanmakU;

public class Enemy : MonoBehaviour, IDamageable
{
    // Editor Variables
    public int Health;
    public float MoveSpeed;

    public DanmakuEmitter[] LaserSystems; // rename to "emitters" once I know what has been assigned here.
    public MissileSystem[] MissileSystems;


    // private void Start()
    // {
    //     //_applyMods(GameDataManager.Instance.EnemyMods);
    // }

    private void FixedUpdate()
    {
        // Move
        transform.position += new Vector3(-MoveSpeed * Time.deltaTime, 0);
    }

    // private void _applyMods(IEnumerable<Mod> mods)
    // {
    //     foreach (Mod mod in mods)
    //     {
    //         // apply that mod to this enemy's stats
    //     }
    // }

    Team IDamageable.Team => Team.Enemy;
    void IDamageable.TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            GameDataManager.Instance.RegisterEnemyDeath(this);
        }

        Destroy(gameObject);
    }
}
