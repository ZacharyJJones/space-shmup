using UnityEngine;

public class MissileSystem : WeaponSystem
{
    // Editor Fields
    // public MissileParams MissileParams;


    private void Awake() => base.OnAwake();
    private void FixedUpdate() => base.OnFixedUpdate();

    public override void PostInstantiation(Projectile missile)
    {
        var homingComponent = missile.GetComponent<Homing>();
        if (homingComponent is null)
            return;

        homingComponent.Initialize(missile);
        homingComponent.SetTarget(EntityManager.Instance.GetRandom(EntityType.Enemy)?.transform);
    }
}
