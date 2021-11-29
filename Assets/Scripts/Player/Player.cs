using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    // Editor Fields
    public int MaxHealth;
    // public LaserSystem LaserSystem;
    // public MissileSystem MissileSystem;

    // Runtime Fields
    private int _currentHealth;

    public void Awake()
    {
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth < 0)
        {
            _currentHealth = MaxHealth;
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
