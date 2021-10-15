using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    // Editor Fields
    public int MaxHealth;
    public LaserSystem LaserSystem;
    public MissileSystem MissileSystem;
    public Player2DController Player2DController;

    // Runtime Fields
    private int _currentHealth { get; set; }

    public void Awake()
    {
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log($"Player took {damage} damage!");

        if (_currentHealth < 0)
        {
            Debug.LogWarning("Player Died!");
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
