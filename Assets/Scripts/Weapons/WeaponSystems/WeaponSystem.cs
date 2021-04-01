using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireGroup
{
    public Transform[] FireLocations;
}

public class WeaponSystem : MonoBehaviour
{
    [Header("Projectile")]
    public GameObject ProjectilePrefab;

    [Header("Fire Groups")]
    public FireGroup[] FireGroups;

    [Header("Params")]
    public WeaponSystemParams Params;



    private float _time;
    private int[] _fireLocsTracking;





    public virtual void OnAwake()
    {
        _fireLocsTracking = new int[FireGroups.Length];
    }

    public virtual void OnUpdate()
    {
        if (!Params.CanFire)
        {
            return;
        }

        _time += Time.deltaTime;
        if (_time >= Params.DelayPerVolley)
        {
            _time = 0f;
            StartCoroutine(_fireVolley());
        }
    }

    public virtual void PostInstantiation(GameObject instantiated)
    {
        // does nothing by default
    }


    public void LoadParams(WeaponSystemParams weaponSystemParams)
    {
        Params = weaponSystemParams;
    }



    IEnumerator _fireVolley()
    { 
        float shotDelayTimer = Params.ShotDelayInVolley;
        for (int shotsFired = 0; shotsFired < Params.ShotsPerVolleyFired; shotsFired++)
        {
            // wait to fire the next shot in the volley until enough time has passed.
            while (shotDelayTimer < Params.ShotDelayInVolley)
            {
                shotDelayTimer += Time.deltaTime;
                yield return null;
            }


            // iterate through weapon groups, firing at next location to be used in that group.
            for (int i = 0; i < FireGroups.Length; i++)
            {
                Transform spawnPoint = FireGroups[i].FireLocations[_fireLocsTracking[i]];

                // increment then constrain value
                _fireLocsTracking[i]++;
                _fireLocsTracking[i] %= FireGroups[i].FireLocations.Length;

                var projectile = Instantiate(ProjectilePrefab);
                projectile.transform.position = spawnPoint.position;
                projectile.transform.rotation = spawnPoint.rotation;

                // set up projectile stuff
                var projComponent = projectile.GetComponent<Projectile>();
                projComponent.Damage = Params.Damage;
                projComponent.Speed = Params.Speed;

                PostInstantiation(projectile);
            }


            // ensure timer gets reduced, as the shot WAS fired.
            shotDelayTimer -= Params.ShotDelayInVolley;
        }
    }



}
