using System.Collections;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    // Editor Fields
    public GameObject ProjectilePrefab;
    public AudioClip FiringAudio;
    public AudioClip ImpactAudio;
    public FireGroup[] FireGroups;
    public WeaponSystemParams Params;

    // Runtime Fields
    private float _time;
    private int[] _fireLocsTracking;


    protected void OnAwake()
    {
        _fireLocsTracking = new int[FireGroups.Length];
    }

    // For inheritors of WeaponSystem to call
    protected void OnFixedUpdate()
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

                // For rotation:
                // - Just using Quaternion.identity did not work
                // - Lerping between these two did work?
                // - maybe just normalize the spawnpoint rotation OR just use the spawnpoint rotation.
                var rotation = Quaternion.Lerp(Quaternion.identity, spawnPoint.rotation, 1f);


                var projectileObj = Instantiate(ProjectilePrefab, spawnPoint.position, rotation, null);
                AudioSource.PlayClipAtPoint(FiringAudio, this.transform.position);

                // set up projectile stuff
                var projectile = projectileObj.GetComponent<Projectile>();
                projectile.Initialize(Params.Damage, Params.Speed, ImpactAudio);

                PostInstantiation(projectile);
            }
        }
    }

    // Empty here, intended to be used by inheritors
    public virtual void PostInstantiation(Projectile projectile) { }
}
