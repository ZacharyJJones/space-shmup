using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Editor Fields
    public Team Team;
    public AudioTrigger ImpactAudio;

    // Runtime Fields
    public int Damage { get; set; }
    public float Speed { get; set; }


    public void Initialize(int damage, float speed)
    {
        Damage = damage;
        Speed = speed;
    }

    private void FixedUpdate()
    {
        //transform.Translate(Speed * Time.deltaTime * transform.right);
        transform.Translate(Speed * Time.deltaTime * Vector3.right);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var damageable = collider.gameObject.GetComponent<IDamageable>();
        if (damageable == null || damageable.Team == Team)
            return;

        damageable.TakeDamage(Damage);
        AudioManager.Instance.PlayTrigger(ImpactAudio);

        var entityComponent = GetComponent<Entity>();
        if (entityComponent != null)
        {
            entityComponent.RemoveSelf();
        }
        Destroy(gameObject);
    }
}
