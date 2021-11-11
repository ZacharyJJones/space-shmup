using System.Collections;
using System;
using UnityEngine;

public static class Utils
{
    public static Vector3 GetRandomPointInCollider(BoxCollider2D collider)
    {
        var bounds = collider.bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        float x = UnityEngine.Random.Range(min.x, max.x);
        float y = UnityEngine.Random.Range(min.y, max.y);

        return new Vector3(x, y);
    }

    /// <summary> Returns the result of Sin(t), normalized such that one wave period occurs over an increment of 1. </summary>
    /// <param name="t"> Input value, used like interpolation, should be between 0-1. </param>
    /// <param name="xOffset"> X offset for sine wave. </param>
    /// <param name="magnitude"> The most extreme y-value deviation from the midline that this function will return. </param>
    /// <param name="midline"> The 'y-value' for the center between the peaks and troughs of the wave. </param>
    public static float Sine(float t, float xOffset = 0, float magnitude = 0.5f, float midline = 0.5f)
    {
        /* sample values for: magnitude 0.5, center @ y=0.5, period = 1
            [0.00 ,   0.50],
            [0.25 ,     1.00],
            [0.50 ,   0.50],
            [0.75 , 0.00],
            [1.00 ,   0.50]
        */
        
        const double TWO_PI = System.Math.PI * 2.0;
        return (float)(System.Math.Sin(TWO_PI * (t + xOffset)) * magnitude + midline);
    }
    
    public static IEnumerator SimpleWait(float waitTime, Action onComplete)
    {
        for (float time = 0; time < waitTime; time += Time.deltaTime)
        {
            yield return null;
        }

        onComplete.Invoke();
    }
    
    public static IEnumerator DoOverTime(float timeToComplete, Action<float> whileActive, Action onComplete = null)
    {
        for (float time = 0; time < timeToComplete; time += Time.deltaTime)
        {
            whileActive.Invoke(time / timeToComplete);
            yield return null;
        }

        onComplete?.Invoke();
    }
}