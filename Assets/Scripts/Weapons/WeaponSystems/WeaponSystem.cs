using System.Collections;
using UnityEngine;

[System.Serializable]
public class FireGroup
{
    public Transform[] FireLocations;
}

public class WeaponSystem : MonoBehaviour
{
    // Editor Fields
    public GameObject ProjectilePrefab;
    public FireGroup[] FireGroups;
    public WeaponSystemParams Params;

    // Runtime Fields
    private float _time;
    private int[] _fireLocsTracking;


    public virtual void OnAwake()
    {
        _fireLocsTracking = new int[FireGroups.Length];
    }

    public virtual void OnFixedUpdate()
    {
        if (!Params.CanFire)
            return;

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


    private IEnumerator _fireVolley()
    {
        float shotDelayTimer = Params.ShotDelayInVolley;
        for (int shotsFired = 0; shotsFired < Params.ShotsPerVolleyFired; shotsFired++)
        {
            while (shotDelayTimer < Params.ShotDelayInVolley)
            {
                shotDelayTimer += Time.deltaTime;
                yield return null;
            }
            shotDelayTimer -= Params.ShotDelayInVolley;

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
                projComponent.Initialize(Params.Damage, Params.Speed);

                PostInstantiation(projectile);
            }
        }
    }
}
