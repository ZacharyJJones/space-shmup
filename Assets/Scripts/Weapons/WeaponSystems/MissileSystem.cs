using System.Collections.Generic;
using UnityEngine;

public class MissileSystem : WeaponSystem
{
    public MissileParams MissileParams;


    void Awake() => base.OnAwake();
    void Update() => base.OnUpdate();



    public MissileSystemParams GetParams()
    {
        return new MissileSystemParams(Params, MissileParams.Clone());
    }

    public void LoadParams(MissileSystemParams missileSysParams, IEnumerable<Mod> mods)
    {
        base.LoadParams(missileSysParams);
        MissileParams = missileSysParams.MissileParams;
    }



    public override void PostInstantiation(GameObject missile)
    {
        var missileComponent = missile.GetComponent<HomingMissile>();
        missileComponent.Params = MissileParams;
        missileComponent.Target = EntityManager.Instance.GetRandom(EntityType.Enemy)?.transform;

        var rampComponent = missile.GetComponent<HomingMissileSpeedRamp>();
        rampComponent.Params = MissileParams;


        AudioManager.Instance.PlayTrigger(AudioTrigger.Missile_Fire);
    }
}
