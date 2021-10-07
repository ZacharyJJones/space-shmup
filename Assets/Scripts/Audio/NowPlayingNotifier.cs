using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowPlayingNotifier : MonoBehaviour
{
    public Text SongNameDisplay;
    public RectTransform NotificationPanel;

    public Vector3 PanelVisiblePosition;
    private Vector3 _panelHiddenPosition;

    public float LerpTime;
    public float VisibleDuration;

    void Start()
    {
        Debug.Log("Starting");
        _panelHiddenPosition = NotificationPanel.position;
    }

    public void ShowPanel(string songName)
    {
        Debug.Log("Panel Beginning to be shown.");
        songName = songName.Replace(" Loop", "");
        SongNameDisplay.text = $"<b>{songName}</b>";
        StartCoroutine(_panelLerpCommon(LerpTime, _panelHiddenPosition, PanelVisiblePosition));
        StartCoroutine(_simpleWait(LerpTime + VisibleDuration, () => _hidePanel()));
    }

    void _hidePanel()
    {
        StartCoroutine(_panelLerpCommon(LerpTime, PanelVisiblePosition, _panelHiddenPosition));
    }

    IEnumerator _simpleWait(float waitTime, Action onComplete)
    {
        Debug.Log("Starting to wait.");
        for (float time = 0; time < waitTime; time += Time.deltaTime)
        {
            yield return null;
        }

        onComplete.Invoke();
    }

    IEnumerator _panelLerpCommon(float duration, Vector3 start, Vector3 end)
    {
        Debug.Log("Starting to Lerp.");
        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            float lerpVal = time / duration;
            NotificationPanel.position = Vector3.Lerp(start, end, lerpVal);

            yield return null;
        }

        Debug.Log("Done Lerping.");
        NotificationPanel.position = end;
    }

}
