using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AvailableMods", menuName = "ScriptableObjects/AvailableMods")]
public class AvailableModsScriptable : ScriptableObject
{

    public ModOption[] Mods;

    public ModSpecial[] SpecialMods;



    public Mod GetRandom(int totalWeight)
    {
        int val = Random.Range(0, totalWeight) + 1;

        int runningTotal = 0;
        foreach (var mod in Mods)
        {
            runningTotal += mod.Weight;
            if (val <= runningTotal)
            {
                return mod.Mod;
            }
        }

        return null;
        // if weighted doesn't work, return
        //return Mods[Random.Range(0, Mods.Length)];
    }


}
