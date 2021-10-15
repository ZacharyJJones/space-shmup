using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRamp : MonoBehaviour
{
    // Editor Fields
    public float SpeedEndFraction;
    public float SpeedChangePeriod;

    // Runtime Fields
    private float _time;
    private Projectile _projectile;

    private void Awake()
    {
        if (_projectile == null)
        {
            _projectile = GetComponent<Projectile>();
        }
    }

    private void FixedUpdate()
    {
        // Increase speed of projectile?
    }
}
