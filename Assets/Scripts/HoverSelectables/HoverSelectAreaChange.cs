using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverSelectAreaChange : HoverSelectableGroup
{
    public Text DisplayText;
    public string[] Areas;


    private void Start()
    {
        InitializeSelectables(Areas, _sendTextSelect);
    }

    public void Initialize(string[] areas, Text areaDisplay)
    {
        Areas = areas;
        DisplayText = areaDisplay;
    }

    private void _sendTextSelect(HoverSelectable selected)
    {
        DisplayText.text += selected.Key;
        EnemyWaveManager.Instance.StartNextWave(this);
    }
}
