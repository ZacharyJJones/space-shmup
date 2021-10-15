using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileSpeedRamp : MonoBehaviour
{
    // Editor Fields
    public MissileParams MissileParams;

    // Runtime Fields
    private Projectile _projectile;
    private float _maxSpeed;
    private float _startSpeed;
    private float _time;


    public void Start()
    {
        _projectile = GetComponent<Projectile>();
        _startSpeed = _projectile.Speed;
        _maxSpeed = _startSpeed * MissileParams.SpeedRampEndMult;
        _time = 0;
    }

    private void FixedUpdate()
    {
        if (_time >= MissileParams.SpeedRampPeriod)
        {
            _projectile.Speed = _maxSpeed;
            Destroy(this);
            return;
        }

        _time += Time.deltaTime;
        float interp = _time / MissileParams.SpeedRampPeriod;
        _projectile.Speed = Mathf.Lerp(_startSpeed, _maxSpeed, interp);
    }
}
