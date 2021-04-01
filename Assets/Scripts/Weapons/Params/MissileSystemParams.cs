[System.Serializable]
public class MissileSystemParams : WeaponSystemParams
{
    public MissileParams MissileParams;


    public MissileSystemParams(MissileParams missileParams, int damage, float speed, int shotsPerVolley, float delayBetweenShots, float delayBetweenVolleys)
        : base(damage, speed, shotsPerVolley, delayBetweenShots, delayBetweenVolleys)
    {
        MissileParams = missileParams;
    }

    public MissileSystemParams(WeaponSystemParams baseParams, MissileParams missileParams)
        : this(missileParams, baseParams.Damage, baseParams.Speed, baseParams.ShotsPerVolleyFired, baseParams.ShotDelayInVolley, baseParams.DelayPerVolley)
    { }
    
    public new MissileSystemParams Clone()
    {
        return new MissileSystemParams(MissileParams.Clone(), Damage, Speed, ShotsPerVolleyFired, ShotDelayInVolley, DelayPerVolley);
    }
}
