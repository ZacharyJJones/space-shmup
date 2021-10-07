using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
  public static Vector3 GetRandomPointInCollider(BoxCollider2D collider)
  {
    Vector3 min = collider.bounds.min;
    Vector3 max = collider.bounds.max;

    float x = Random.Range(min.x, max.x);
    float y = Random.Range(min.y, max.y);

    return new Vector3(x, y);
  }
}
