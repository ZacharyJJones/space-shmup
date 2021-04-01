using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowPlayingNotifier : MonoBehaviour
{
    public RectTransform NotificationPanel;
    public Vector3 PanelVisiblePosition;
    public Text SongNameDisplay;
    public float LerpTime;

    // how long the panel is visible for before being hidden
    public float VisibleDuration;


    private Vector3 _panelHiddenPosition;




    void Awake()
    {
        _panelHiddenPosition = NotificationPanel.position;
    }


    public void ShowPanel(string songName)
    {
        songName = songName.Replace(" Loop", "");
        SongNameDisplay.text = $"<b>{songName}</b>";
        _showPanel();
    }



    void _showPanel()
    {
        StartCoroutine(_panelLerpCommon(LerpTime, _panelHiddenPosition, PanelVisiblePosition));
        StartCoroutine(_simpleWait(LerpTime + VisibleDuration, () => _hidePanel()));
    }
    
    void _hidePanel()
    {
        StartCoroutine(_panelLerpCommon(LerpTime, PanelVisiblePosition, _panelHiddenPosition));
    }

    IEnumerator _simpleWait(float waitTime, Action onComplete)
    {
        for (float time = 0; time < waitTime; time += Time.deltaTime)
        {
            yield return null;
        }

        onComplete.Invoke();
    }

    IEnumerator _panelLerpCommon(float duration, Vector3 start, Vector3 end)
    {
        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            float lerpVal = time / duration;
            NotificationPanel.position = Vector3.Lerp(start, end, lerpVal);

            yield return null;
        }

        NotificationPanel.position = Vector3.Lerp(start, end, 1f);
    }

}
