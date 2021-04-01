using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public LaserSystem LaserSystem;
    public MissileSystem MissileSystem;
    public Player2DController Player2DController;







    [HideInInspector]
    public int MaxHealth;
    public int Health { get; set; }



    // allows for turning off weapon systems
    //private bool _canFire;
    //public bool CanFire
    //{
    //    get { return _canFire; }
    //    set
    //    {
    //        _canFire = value;
    //        LaserSystem.Params.CanFire = value;
    //        MissileSystem.Params.CanFire = value;
    //    }
    //}

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"Player took {damage} damage!");

        if (Health < 0)
        {
            Debug.LogWarning("Player Died!");
            Health = MaxHealth;
        }

        // if dead, do something
    }

    // this handles receiving non-danmaku hits.
    Team IDamageable.Team => Team.Player;
    void IDamageable.TakeDamage(int damage)
    {
        TakeDamage(damage);
    }
}
