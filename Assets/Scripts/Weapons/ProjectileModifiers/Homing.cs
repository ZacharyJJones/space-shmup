using System;
using UnityEngine;

public class Homing : MonoBehaviour, IProjectileModifier
{
    // Editor Fields
    public GameObject Target; // usually set by the thing firing the homing projectile.
    public float TurnRate;
    public float WigglePeriod = 2f;
    [Range(0, 180)]
    public float WiggleMaxTurnDelta;

    // Runtime Fields
    private float _time;


    private void Start()
    {
        _time = UnityEngine.Random.Range(0f, WigglePeriod);
    }

    private void FixedUpdate()
    {
        if (!Target || !Target.activeSelf)
        {
            Destroy(this);
            return;
        }
        _rotateToTarget();
    }

    public void Initialize(Projectile projectile) { }
    public void SetTarget(Entity target)
    {
        if (target && target != null)
        {
            Target = target.gameObject;
        }
    }

    private void _rotateToTarget()
    {
        _time += Time.fixedDeltaTime;
        if (_time > WigglePeriod)
            _time -= WigglePeriod;

        float targetHeading = Utils.HeadingFromNormalizedVector((Target.transform.position - transform.position).normalized);

        targetHeading += Utils.Sine(_time / WigglePeriod, 0, 1, 0) * WiggleMaxTurnDelta;

        var newRotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0, 0, targetHeading),
            TurnRate * Time.deltaTime
        ).normalized;

        transform.rotation = newRotation;
    }
}
