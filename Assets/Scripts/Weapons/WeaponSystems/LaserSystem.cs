using System.Collections.Generic;
using UnityEngine;

public class LaserSystem : WeaponSystem
{
    public float InaccuracyInDegrees;


    void Awake() => base.OnAwake();
    void Update() => base.OnUpdate();


    public LaserSystemParams GetParams()
    {
        return new LaserSystemParams(Params, InaccuracyInDegrees);
    }

    public void LoadParams(LaserSystemParams laserSysParams, IEnumerable<Mod> mods)
    {
        base.LoadParams(laserSysParams);
        InaccuracyInDegrees = laserSysParams.InaccuracyInDegrees;
    }


    public override void PostInstantiation(GameObject laser)
    {
        float zEulerAdjust = Random.Range(-InaccuracyInDegrees / 2f, InaccuracyInDegrees / 2f);

        var eulers = laser.transform.rotation.eulerAngles;
        laser.transform.rotation = Quaternion.Euler(eulers.x, eulers.y, eulers.z + zEulerAdjust);

        AudioManager.Instance.PlayTrigger(AudioTrigger.Laser_Player);
    }
}
