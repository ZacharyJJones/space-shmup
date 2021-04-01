using UnityEngine;

[System.Serializable]
public class MissileParams
{
    public float TurnRate;
    public float SpeedRampPeriod;
    public float SpeedRampEndMult;



    public MissileParams Clone() => new MissileParams(TurnRate, SpeedRampPeriod, SpeedRampEndMult);
    public MissileParams(float turnRate, float speedRampPeriod, float speedRampEndMult)
    {
        TurnRate = turnRate;
        SpeedRampPeriod = speedRampPeriod;
        SpeedRampEndMult = speedRampEndMult;
    }
}