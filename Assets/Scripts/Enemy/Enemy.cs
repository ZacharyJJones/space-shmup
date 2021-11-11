using UnityEngine;
using DanmakU;

public class Enemy : MonoBehaviour, IDamageable
{
    // Editor Variables
    public int Health;
    public float MoveSpeed;

    // Runtime Variables
    private int _currentHealth;
    

    private void Start()
    {
        _currentHealth = Health;
    }

    private void FixedUpdate()
    {
        transform.Translate(MoveSpeed * Time.deltaTime * Vector3.right);
    }

    Team IDamageable.Team => Team.Enemy;
    void IDamageable.TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0)
        {
            GameDataManager.Instance.RegisterEnemyDeath(this);
        }
        Destroy(gameObject);
    }
}
