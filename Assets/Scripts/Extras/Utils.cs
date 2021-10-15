using UnityEngine;

public static class Utils
{
  public static Vector3 GetRandomPointInCollider(BoxCollider2D collider)
  {
    var bounds = collider.bounds;
    Vector3 min = bounds.min;
    Vector3 max = bounds.max;

    float x = Random.Range(min.x, max.x);
    float y = Random.Range(min.y, max.y);

    return new Vector3(x, y);
  }
}
