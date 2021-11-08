using UnityEngine;

public class LaserSystem : WeaponSystem
{
    // Editor Fields
    public float InaccuracyInDegrees;
    
    // Runtime fields
    private float _halfInaccuracy;


    private void Awake()
    {
        base.OnAwake();
        _halfInaccuracy = InaccuracyInDegrees / 2f;
    }
    private void FixedUpdate() => base.OnFixedUpdate();

    public override void PostInstantiation(Projectile laser)
    {
        float inaccuracyAdjustment = Random.Range(-_halfInaccuracy, _halfInaccuracy);

        var eulers = laser.transform.rotation.eulerAngles;
        laser.transform.rotation = Quaternion.Euler(eulers.x, eulers.y, eulers.z + inaccuracyAdjustment);
    }
}
