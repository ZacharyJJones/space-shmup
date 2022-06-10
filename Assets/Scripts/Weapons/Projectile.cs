using UnityEngine;

public class Projectile : Entity
{
    public override EntityType Type => EntityType.Projectile;

    // Editor Fields
    public Team Team; // seems like this should be set by the thing firing the projectile?
    public Rigidbody2D Rigidbody;

    // Properties, to not show up in editor. Public, because access needed by other components
    public int Damage { get; set; }
    public float Speed { get; set; }
    private AudioClip _impactAudio;


    public void Initialize(int damage, float speed, AudioClip impactAudio)
    {
        Damage = damage;
        Speed = speed;
        _impactAudio = impactAudio;
    }

    private void FixedUpdate()
    {
        transform.Translate(Speed * Time.deltaTime * Vector3.right);
        Rigidbody.MovePosition(Rigidbody.position);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var damageable = collider.gameObject.GetComponent<IDamageable>();
        if (damageable == null || damageable.Team == Team)
            return;

        damageable.TakeDamage(Damage);
        AudioSource.PlayClipAtPoint(_impactAudio, this.transform.position);

        var entityComponent = GetComponent<Entity>();
        if (entityComponent)
        {
            entityComponent.RemoveSelf();
        }
        Destroy(gameObject);
    }


}
