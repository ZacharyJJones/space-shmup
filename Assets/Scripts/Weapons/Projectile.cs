using UnityEngine;

public class Projectile : Entity
{
    // Editor Fields
    public Team Team;
    public AudioTrigger ImpactAudio;
    public Rigidbody2D RigidBody;

    // Runtime Fields
    public int Damage { get; set; }
    public float Speed { get; set; }

    public override EntityType Type => EntityType.Projectile;


    private void FixedUpdate()
    {
        transform.Translate(Speed * Time.deltaTime * Vector3.right);
        RigidBody.MovePosition(RigidBody.position);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var damageable = collider.gameObject.GetComponent<IDamageable>();
        if (damageable == null || damageable.Team == Team)
            return;

        damageable.TakeDamage(Damage);
        AudioManager.Instance.PlayTrigger(ImpactAudio);

        var entityComponent = GetComponent<Entity>();
        if (entityComponent)
        {
            entityComponent.RemoveSelf();
        }
        Destroy(gameObject);
    }


    public void Initialize(int damage, float speed)
    {
        Damage = damage;
        Speed = speed;
    }
}
