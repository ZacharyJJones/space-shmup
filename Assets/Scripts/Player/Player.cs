using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IDamageable
{
    public override EntityType Type => EntityType.Player;


    // Editor Fields
    public int MaxHealth;

    // Runtime Fields
    private int _currentHealth;
    private SceneLoad _sceneLoad;

    protected void Awake()
    {
        _sceneLoad = GetComponent<SceneLoad>();
    }

    protected override void Start()
    {
        base.Start();
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. HP is now {_currentHealth}.");

        if (_currentHealth <= 0)
        {
            _currentHealth = MaxHealth;
            _sceneLoad.LoadScene();
        }
    }

    // this handles receiving non-danmaku hits.
    Team IDamageable.Team => Team.Player;
    void IDamageable.TakeDamage(int damage)
    {
        TakeDamage(damage);
    }
}
