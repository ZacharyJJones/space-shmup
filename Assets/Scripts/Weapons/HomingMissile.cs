using DanmakU;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public const float MISSILE_WIGGLE_FRACTION = 0.3f;
    public const float MISSILE_WIGGLE_PERIOD = 2f;



    public Transform Target;

    [HideInInspector]
    public MissileParams Params;


    // hidden stuff
    private float _time;



    void Start()
    {
        _time = UnityEngine.Random.Range(0f, MISSILE_WIGGLE_PERIOD);
    }


    void FixedUpdate()
    {
        _setRotation();
    }


    private void _setRotation()
    {
        if (Target == null)
        {
            return;
        }

        float delta = Time.deltaTime;

        Vector2 diff = (Target.position - transform.position);
        float degreesRotatableThisFrame = Params.TurnRate * delta;

        var unitCircle = diff.normalized;

        float headingToTarget = 0f;
        if (unitCircle.y < 0)
        {
            // unity rotation is +90 degrees when compared to unit circle. thus 'x' is the 180 degree marker.
            // in bottom half, so add 180 degrees and flip x

            headingToTarget += 180f;
            unitCircle.x *= -1f;
        }

        // figure out the degrees value for X
        headingToTarget += Mathf.Asin(unitCircle.x) * Mathf.Rad2Deg;


        // add wiggle amount to final rotation.
        _time += delta;
        if (_time > MISSILE_WIGGLE_PERIOD)
            _time -= MISSILE_WIGGLE_PERIOD;

        float sineT = (_time / MISSILE_WIGGLE_PERIOD);
        float wiggleMagnitude = Sine(sineT, 0, 1, 0);
        float totalWiggle = wiggleMagnitude * MISSILE_WIGGLE_FRACTION * Params.TurnRate;


        headingToTarget += totalWiggle;

        // unsure of why headingToTarget needs to be inverted here, but it IS necessary.
        var desiredRotation = Quaternion.Euler(0, 0, -headingToTarget);


        var newRotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, degreesRotatableThisFrame).normalized;


        transform.rotation = newRotation;
    }

    /// <summary> Returns the result of Sin(t), normalized such that one wave period occurs over an increment of 1. </summary>
    /// <param name="t"> Input value, used like interpolation, should be between 0-1. </param>
    /// <param name="xOffset"> X offset for sine wave. </param>
    /// <param name="magnitude"> The most extreme y-value deviation from the midline that this function will return. </param>
    /// <param name="midline"> The 'y-value' for the center between the peaks and troughs of the wave. </param>
    public static float Sine(float t, float xOffset = 0, float magnitude = 0.5f, float midline = 0.5f)
    {
        // 'sine' produces the result of Math.Sin, which (by default) is normalized to the 0-1 domain/range.
        // https://www.desmos.com/calculator/xp5uwiwywl

        // magnitude 0.5, center @ y=0.5, period = 1
        /* sample values:

        [0.00 ,   0.50],
        [0.25 ,     1.00],
        [0.50 ,   0.50],
        [0.75 , 0.00],
        [1.00 ,   0.50]

        */
        double ret = (Math.Sin(TWO_PI * (t + xOffset)) * magnitude) + midline;
        return (float)ret;
    }
    public const double TWO_PI = Math.PI * 2.0;



}
