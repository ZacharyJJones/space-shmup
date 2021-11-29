using System;
using UnityEngine;

public class Homing : MonoBehaviour, IProjectileModifier
{
    // Editor Fields
    public Transform Target; // usually set by the thing firing the homing projectile.
    public float TurnRate;
    [Range(0, 180)]
    public float WiggleMaxTurnDelta;
    public float WigglePeriod = 2f;

    // Runtime Fields
    private float _time;

    private void Start()
    {
        _time = UnityEngine.Random.Range(0f, WigglePeriod);
    }

    private void FixedUpdate()
    {
        if (!Target)
        {
            Destroy(this);
            return;
        }
        _rotateToTarget();
    }

    public void Initialize(Projectile projectile) { }
    public void SetTarget(Transform target)
    {
        Target = target;
    }

    private void _rotateToTarget()
    {
        _time += Time.fixedDeltaTime;
        if (_time > WigglePeriod)
            _time -= WigglePeriod;

        float targetHeading = Utils.HeadingFromNormalizedVector((Target.position - transform.position).normalized);

        targetHeading += Utils.Sine(_time / WigglePeriod, 0, 1, 0) * WiggleMaxTurnDelta;

        var newRotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0, 0, targetHeading),
            TurnRate * Time.deltaTime
        ).normalized;

        transform.rotation = newRotation;
    }
}
