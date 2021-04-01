using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileSpeedRamp : MonoBehaviour
{
    public MissileParams Params;


    private Projectile _projectile;
    private float _maxSpeed;
    private float _startSpeed;
    private float _time;


    // Start is called before the first frame update
    void Start()
    {
        _projectile = GetComponent<Projectile>();
        _startSpeed = _projectile.Speed;
        _maxSpeed = _startSpeed * Params.SpeedRampEndMult;
        _time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time >= Params.SpeedRampPeriod)
        {
            _projectile.Speed = _maxSpeed;
            Destroy(this);
            return;
        }

        _time += Time.deltaTime;
        float interp = _time / Params.SpeedRampPeriod;

        _projectile.Speed = Mathf.Lerp(_startSpeed, _maxSpeed, interp);        
    }
}
