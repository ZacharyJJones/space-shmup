using System.Collections;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    // Editor Fields
    public GameObject ProjectilePrefab;
    public AudioTrigger FiringSound;
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

        _time += Time.fixedDeltaTime;
        if (_time >= Params.DelayPerVolley)
        {
            _time = 0f;
            StartCoroutine(_fireVolley());
        }
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
                //
                // var gameObject = Instantiate(
                //     ProjectilePrefab, 
                //     spawnPoint.position,
                //     Quaternion.Euler(0, 0, spawnPoint.rotation.eulerAngles.z),
                //     null
                // );
                var rotation = Quaternion.Lerp(Quaternion.identity, spawnPoint.rotation, 1f);
                // var gameObject = Instantiate(ProjectilePrefab, spawnPoint.position, Quaternion.identity, null);
                var gameObject = Instantiate(ProjectilePrefab, spawnPoint.position, rotation, null);

                // set up projectile stuff
                var projectile = gameObject.GetComponent<Projectile>();
                projectile.Initialize(Params.Damage, Params.Speed);
                AudioManager.Instance.PlayTrigger(FiringSound);

                PostInstantiation(projectile);
            }
        }
    }
    
    public virtual void PostInstantiation(Projectile instantiated)
    {
        // empty for children to add functionality if needed
    }
}
