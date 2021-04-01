using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanmakU;

public class Enemy : MonoBehaviour, IDamageable
{
    public int Health;
    public float MoveSpeed;



    public DanmakuEmitter[] LaserSystems; // not actually laser systems, but in the context of the game they are.
    public MissileSystem[] MissileSystems;




    void Start()
    {
        _applyMods(GameDataManager.Instance.EnemyMods);
    }

    void FixedUpdate()
    {
        _move();
    }

    void _applyMods(IEnumerable<Mod> mods)
    {
        foreach (Mod mod in mods)
        {
            // apply that mod to this enemy's stats
        }
    }

    private void _move() => transform.position += new Vector3(-MoveSpeed * Time.deltaTime, 0);








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
