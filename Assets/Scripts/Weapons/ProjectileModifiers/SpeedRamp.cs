using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRamp : MonoBehaviour, IProjectileModifier
{
    // Editor Fields
    public float SpeedEndFraction;
    public float SpeedChangePeriod;
    
    [Range(1,2)]
    public int LerpPower;

    // Runtime Fields
    private float _initialSpeed;
    private float _endSpeed;
    private float _time;
    private Projectile _projectile;

    public void Initialize(Projectile projectile)
    {
        _projectile = projectile;
        _initialSpeed = projectile.Speed;
        _endSpeed = projectile.Speed * SpeedEndFraction;
        _time = 0f;
    }

    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        float rampPercentage = Mathf.Min(1f, _time / SpeedChangePeriod);
        if (LerpPower == 2)
            rampPercentage *= rampPercentage;
        
        _projectile.Speed = Mathf.Lerp(_initialSpeed, _endSpeed, rampPercentage);
        
        if (rampPercentage >= 1)
            Destroy(this);
    }

}
