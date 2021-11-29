using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public float TimeBetweenSpawns;

    private float _time;

    private void Start()
    {
        _time = Random.Range(0, TimeBetweenSpawns);
    }

    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        if (_time <= TimeBetweenSpawns)
            return;

        Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
        _time -= TimeBetweenSpawns;
    }
}
