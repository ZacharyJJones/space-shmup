[System.Serializable]
public class LaserSystemParams : WeaponSystemParams
{
    public float InaccuracyInDegrees;


    public LaserSystemParams(float inacccuracy, int damage, float speed, int shotsPerVolley, float delayBetweenShots, float delayBetweenVolleys)
        : base(damage, speed, shotsPerVolley, delayBetweenShots, delayBetweenVolleys)
    {
        InaccuracyInDegrees = inacccuracy;
    }

    public LaserSystemParams(WeaponSystemParams baseParams, float inaccuracy)
        : this(inaccuracy, baseParams.Damage, baseParams.Speed, baseParams.ShotsPerVolleyFired, baseParams.ShotDelayInVolley, baseParams.DelayPerVolley)
    { }

    public new LaserSystemParams Clone() 
    {
        return new LaserSystemParams(InaccuracyInDegrees, Damage, Speed, ShotsPerVolleyFired, ShotDelayInVolley, DelayPerVolley); 
    }
}
