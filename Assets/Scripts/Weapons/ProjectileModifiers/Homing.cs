using System;
using UnityEngine;

public class Homing : MonoBehaviour, IProjectileModifier
{
    // when moving wiggle to own class, these would be editor values.
    // -- does wiggle really need to be divorced from homing?
    // -- It seems more like a variation of homing than it's own behavior.
    public const float MISSILE_WIGGLE_FRACTION = 0.3f;
    public const float MISSILE_WIGGLE_PERIOD = 2f;

    public float TurnRate;
    
    // Runtime Fields
    private Projectile _projectile;
    private Transform _target;
    private float _time;

    public void Awake()
    {
        _time = UnityEngine.Random.Range(0f, MISSILE_WIGGLE_PERIOD);
    }
    
    private void FixedUpdate()
    {
        if (_target)
            _rotateToTarget();
    }

    
    public void Initialize(Projectile projectile)
    {
        _projectile = projectile;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void _rotateToTarget()
    {
        // In unity, 0 degrees is +x, 90 degrees is -y, 180 degrees is -x, 270 degrees is +y.
        Vector2 unitCircle = (_target.position - transform.position).normalized;

        // figure out the degrees value for X
        float headingToTarget = 0f;
        if (unitCircle.y < 0)
        {
            // in bottom half of unit circle, so add 180 degrees and flip x
            headingToTarget += 180f;
            unitCircle.x *= -1f;
        }
        headingToTarget += Mathf.Asin(unitCircle.x) * Mathf.Rad2Deg;

        // add wiggle amount to final rotation.
        _time += Time.deltaTime;
        if (_time > MISSILE_WIGGLE_PERIOD)
            _time -= MISSILE_WIGGLE_PERIOD;

        // totalWiggle gets multiplied by (TurnRate * WiggleFraction) so that it doesn't overpower the homing 
        float wiggleMagnitude = Utils.Sine(_time / MISSILE_WIGGLE_PERIOD, 0, 1, 0);
        float totalWiggle = wiggleMagnitude * MISSILE_WIGGLE_FRACTION * TurnRate;

        // unsure of why headingToTarget needs to be inverted here, but it IS necessary.
        headingToTarget = (headingToTarget + totalWiggle) * -1;

        var desiredRotation = Quaternion.Euler(0, 0, headingToTarget);
        float degreesRotatableThisFrame = TurnRate * Time.deltaTime;
        var newRotation = Quaternion.RotateTowards(
            transform.rotation,
            desiredRotation,
            degreesRotatableThisFrame
        ).normalized;

        transform.rotation = newRotation;
    }
}
