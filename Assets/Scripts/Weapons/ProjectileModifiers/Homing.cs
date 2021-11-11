using System;
using UnityEngine;

public class Homing : MonoBehaviour, IProjectileModifier
{
    // Editor Fields
    public float TurnRate;
    public float WiggleFraction = 0.3f;
    public float WigglePeriod = 2f;

    // Runtime Fields
    private Transform _target;
    private float _time;

    private void Start()
    {
        _time = UnityEngine.Random.Range(0f, WigglePeriod);
    }

    private void FixedUpdate()
    {
        if (_target)
            _rotateToTarget();
    }
    
    public void Initialize(Projectile projectile) { }
    public void SetTarget(Transform target)
    {
        _target = target;
        Debug.Log($"Target is null? {_target == null}");
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
        if (_time > WigglePeriod)
            _time -= WigglePeriod;

        // totalWiggle gets multiplied by (TurnRate * WiggleFraction) so that it doesn't overpower the homing 
        float wiggleMagnitude = Utils.Sine(_time / WigglePeriod, 0, 1, 0);
        float totalWiggle = wiggleMagnitude * WiggleFraction * TurnRate;

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
