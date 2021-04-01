[System.Serializable]
public class WeaponSystemParams
{
    public int Damage;
    public float Speed;

    public bool CanFire;

    public float DelayPerVolley;
    public int ShotsPerVolleyFired;
    public float ShotDelayInVolley;



    public WeaponSystemParams Clone() => new WeaponSystemParams(Damage, Speed, ShotsPerVolleyFired, ShotDelayInVolley, DelayPerVolley);
    public WeaponSystemParams(int damage, float speed, int shotsPerVolley, float delayBetweenShots, float delayBetweenVolleys)
    {
        Damage = damage;
        Speed = speed;
        ShotsPerVolleyFired = shotsPerVolley;
        ShotDelayInVolley = delayBetweenShots;
        DelayPerVolley = delayBetweenVolleys;

        CanFire = true;
    }
}
