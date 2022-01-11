using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HoverSelectable))]
public class HoverSelectAreaChange : MonoBehaviour
{
    public string Area;
    public Text DisplayText;

    private void Start()
    {
        var selectable = GetComponent<HoverSelectable>();
        selectable.OnSelected += _sendTextSelect;
    }

    private void _sendTextSelect(object sender)
    {
        EnemyWaveManager.Instance.StartNextWave();
        DisplayText.text += Area;
    }
}
