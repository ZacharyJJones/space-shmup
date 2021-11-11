using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
public class NowPlayingNotifier : MonoBehaviour
{
    // Editor Fields
    public Text SongNameDisplay;
    public RectTransform NotificationPanel;

    public Vector3 PanelVisiblePosition;
    private Vector3 _panelHiddenPosition;

    public float LerpTime;
    public float VisibleDuration;


    private void Start()
    {
        _panelHiddenPosition = NotificationPanel.position;
    }

    public void NotifyPlayingSong(string songName)
    {
        // Formatting
        SongNameDisplay.text = $"<b>{songName}</b>";

        // Show Panel right away
        StartCoroutine(_panelLerpCommon(LerpTime, _panelHiddenPosition, PanelVisiblePosition));
            
        // Schedule to Hide Panel
        StartCoroutine(Utils.SimpleWait(LerpTime + VisibleDuration, _hidePanel));
    }
    
    private void _hidePanel()
    {
        StartCoroutine(_panelLerpCommon(LerpTime, PanelVisiblePosition, _panelHiddenPosition));
    }
    
    private IEnumerator _panelLerpCommon(float duration, Vector3 start, Vector3 end)
    {
        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            NotificationPanel.position = Vector3.Lerp(start, end, time / duration);
            yield return null;
        }

        NotificationPanel.position = end;
    }
}
