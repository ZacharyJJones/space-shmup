using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IDamageable
{
    // Editor Fields
    public int MaxHealth;

    // Runtime Fields
    private int _currentHealth;

    public override EntityType Type => EntityType.Player;


    protected override void Start()
    {
        base.Start();
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth < 0)
        {
            _currentHealth = MaxHealth;
        }

        Debug.Log($"Player took {damage} damage. HP is now {_currentHealth}.");
    }

    // this handles receiving non-danmaku hits.
    Team IDamageable.Team => Team.Player;
    void IDamageable.TakeDamage(int damage)
    {
        TakeDamage(damage);
    }
}
