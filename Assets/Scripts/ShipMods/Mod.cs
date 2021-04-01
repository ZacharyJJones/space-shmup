using UnityEngine;

[System.Serializable]
public class Mod
{
    public ModType Type;
    public float PlayerValue;
    public float EnemyValue;

    public float GetValue(Team team) => (team == Team.Player) ? PlayerValue : EnemyValue;
    public int GetValueInt(Team team) => Mathf.RoundToInt((team == Team.Player) ? PlayerValue : EnemyValue);
}