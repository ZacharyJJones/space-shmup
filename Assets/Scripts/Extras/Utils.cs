using System;
using System.Collections;
using UnityEngine;

public static class Utils
{
    public static int GetRandomNoRepeat(int maxExclusive, int last)
    {
        int rand = UnityEngine.Random.Range(0, maxExclusive);
        return (last + 1 + rand) % maxExclusive;
    }

    public static Vector3 GetRandomPointInCollider(BoxCollider2D collider)
    {
        var bounds = collider.bounds;
        var (minBound, maxBound) = (bounds.min, bounds.max);

        float x = UnityEngine.Random.Range(minBound.x, maxBound.x);
        float y = UnityEngine.Random.Range(minBound.y, maxBound.y);

        return new Vector3(x, y);
    }

    public static bool IsPointInCollider(Vector3 position, BoxCollider2D collider)
    {
        var bounds = collider.bounds;
        var (minBound, maxBound) = (bounds.min, bounds.max);
        var (x, y) = (position.x, position.y);
        return (minBound.x <= x && x <= maxBound.x) && (minBound.y <= y && y <= maxBound.y);
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
