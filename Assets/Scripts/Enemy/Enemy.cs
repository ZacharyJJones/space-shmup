using UnityEngine;
using DanmakU;

public class Enemy : Entity, IDamageable
{
    private const float DEATH_TIME = 5f;

    // Editor Variables
    public int Health;
    public float MoveSpeed;

    // Runtime Variables
    private int _currentHealth;

    public override EntityType Type => EntityType.Enemy;


    protected override void Start()
    {
        base.Start();
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
            EntityManager.Instance.RemoveEntity(Type, this);
            gameObject.SetActive(false);
            Destroy(gameObject, DEATH_TIME);
        }
    }
}
