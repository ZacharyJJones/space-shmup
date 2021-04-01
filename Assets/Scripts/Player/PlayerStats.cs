using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Player Player;


    // these are gathered when starting.
    [HideInInspector] public int BaseMaxHealth;
    [HideInInspector] public float BaseMoveSpeed;
    [HideInInspector] public LaserSystemParams BaseLaserSystemParams;
    [HideInInspector] public MissileSystemParams BaseMissileSystemParams;



    void Awake()
    {
        BaseMaxHealth = Player.MaxHealth;
        BaseMoveSpeed = Player.Player2DController.MoveSpeed;

        BaseLaserSystemParams = Player.LaserSystem.GetParams();
        BaseMissileSystemParams = Player.MissileSystem.GetParams();
    }

    void Start()
    {
        RecalculatePlayerStats(null);
    }


    public void RecalculatePlayerStats(IEnumerable<Mod> playerMods)
    {
        Player.MaxHealth = BaseMaxHealth;
        Player.Player2DController.MoveSpeed = BaseMoveSpeed;
        Player.LaserSystem.Params = BaseLaserSystemParams.Clone();
        Player.MissileSystem.Params = BaseMissileSystemParams.Clone();

        if (playerMods == null)
        {
            return;
        }

        // apply all the mods to the relevant stats
        foreach (var mod in playerMods)
        {
            // this should be all that needs to happen here.
            _applyMod(mod);
        }
    }

    private void _applyMod(Mod mod)
    {
        var team = Team.Player;
        switch (mod.Type)
        {
            case ModType.Health_Max:
                Player.MaxHealth += mod.GetValueInt(team);
                break;

            case ModType.MoveSpeed:
                Player.Player2DController.MoveSpeed += mod.GetValue(team);
                break;



            case ModType.Laser_Damage:
                Player.LaserSystem.Params.Damage += mod.GetValueInt(team);
                break;

            case ModType.Laser_Speed:
                Player.LaserSystem.Params.Speed += mod.GetValue(team);
                break;

            case ModType.Laser_Accuracy:
                Player.LaserSystem.InaccuracyInDegrees += mod.GetValue(team);
                break;

            case ModType.Laser_Volley_Delay:
                Player.LaserSystem.Params.DelayPerVolley += mod.GetValue(team);
                break;

            case ModType.Laser_Volley_Shot_Count:
                Player.LaserSystem.Params.ShotsPerVolleyFired += mod.GetValueInt(team);
                break;

            case ModType.Laser_Volley_Shot_Delay:
                Player.LaserSystem.Params.ShotDelayInVolley += mod.GetValue(team);
                break;



            case ModType.Missile_Damage:
                Player.LaserSystem.Params.Damage += mod.GetValueInt(team);
                break;

            case ModType.Missile_Speed:
                Player.MissileSystem.Params.Speed += mod.GetValue(team);
                break;

            case ModType.Missile_Turn_Rate:
                Player.MissileSystem.MissileParams.TurnRate += mod.GetValue(team);
                break;

            case ModType.Missile_Volley_Delay:
                Player.MissileSystem.Params.DelayPerVolley += mod.GetValue(team);
                break;

            case ModType.Missile_Volley_Shot_Count:
                Player.MissileSystem.Params.ShotsPerVolleyFired += mod.GetValueInt(team);
                break;

            case ModType.Missile_Volley_Shot_Delay:
                Player.MissileSystem.Params.ShotDelayInVolley += mod.GetValue(team);
                break;



            default:
                break;
        }
    }



}
