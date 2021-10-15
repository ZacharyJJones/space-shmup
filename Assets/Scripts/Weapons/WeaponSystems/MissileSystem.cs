using UnityEngine;

public class MissileSystem : WeaponSystem
{
    // Editor Fields
    public MissileParams MissileParams;


    private void Awake() => base.OnAwake();
    private void FixedUpdate() => base.OnFixedUpdate();

    public override void PostInstantiation(GameObject missile)
    {
        var missileComponent = missile.GetComponent<HomingMissile>();
        missileComponent.Initialize(
            EntityManager.Instance.GetRandom(EntityType.Enemy)?.transform,
            MissileParams
        );

        var rampComponent = missile.GetComponent<HomingMissileSpeedRamp>();
        rampComponent.MissileParams = MissileParams;

        AudioManager.Instance.PlayTrigger(AudioTrigger.Missile_Fire);
    }
}
