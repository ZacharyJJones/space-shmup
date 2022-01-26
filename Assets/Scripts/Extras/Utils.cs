using System;
using System.Collections;
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

    public static bool IsPointInCollider(Vector3 position, BoxCollider2D collider)
    {
        var bounds = collider.bounds;
        return (position.x >= bounds.min.x && position.x <= bounds.max.x)
               && (position.y >= bounds.min.y && position.y <= bounds.max.y);
    }

    public static Vector3 ConstrainWithinCollider(Vector3 desiredPosition, BoxCollider2D collider)
    {
        var bounds = collider.bounds;
        return new Vector3(
            Mathf.Clamp(desiredPosition.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(desiredPosition.y, bounds.min.y, bounds.max.y),
            desiredPosition.z)
        ;
    }


    /// <summary> Returns the heading (North being 0 degrees, clockwise rotation) equivalent for the vector given. </summary>
    /// <param name="normalizedVectorToTarget"> The normalized vector from which a heading is to be determined. </param>
    public static float HeadingFromNormalizedVector(Vector3 normalizedVectorToTarget)
    {
        float headingToTarget = Mathf.Acos(normalizedVectorToTarget.x) * Mathf.Rad2Deg;
        if (normalizedVectorToTarget.y < 0)
            headingToTarget = 360 - headingToTarget;

        return headingToTarget;
    }

    public static IEnumerator SimpleWait(float waitTime, Action onComplete)
    {
        for (float time = 0; time < waitTime; time += Time.deltaTime)
        {
            yield return null;
        }

        onComplete?.Invoke();
    }

    public static IEnumerator SimpleWaitConditional(Func<bool> completeWhenTrue, Action onComplete)
    {
        while (!completeWhenTrue.Invoke())
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
