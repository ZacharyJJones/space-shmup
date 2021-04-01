using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Team Team;
    public int Damage;
    public float Speed;


    void FixedUpdate()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var damageable = collider.gameObject.GetComponent<IDamageable>();
        if (damageable == null || damageable.Team == Team)
        {
            return;
        }


        damageable.TakeDamage(Damage);

        AudioManager.Instance.PlayTrigger(AudioTrigger.Laser_Impact);

        // destroy bullet
        var entityComponent = GetComponent<Entity>();
        if (entityComponent != null)
        {
            entityComponent.RemoveSelf();
        }

        Destroy(gameObject);
    }
}


