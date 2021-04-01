using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRamp : MonoBehaviour
{
    public float SpeedEndFraction;
    public float SpeedChangePeriod;

    private float _time;
    private Projectile _projectile;

    // Start is called before the first frame update
    void Awake()
    {
        if (_projectile == null)
        {
            _projectile = GetComponent<Projectile>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
