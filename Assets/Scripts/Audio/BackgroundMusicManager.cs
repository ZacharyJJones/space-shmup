using System;
using System.Collections;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Range(0, 1)]
    public AudioSource Source;
    public NowPlayingNotifier NowPlayingNotifier;
    public BackgroundMusic[] BackgroundMusic;

    private int _indexPlaying;


    private void Awake()
    {
        _indexPlaying = -1;
        Source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(_scheduleMusicToPlay(0f));
    }

    private IEnumerator _scheduleMusicToPlay(float timeUntilStart)
    {
        while (timeUntilStart > 0)
        {
            timeUntilStart -= Time.deltaTime;
            yield return null;
        }

        _indexPlaying = Utils.GetRandomNoRepeat(BackgroundMusic.Length, _indexPlaying);
        var selectedMusic = BackgroundMusic[_indexPlaying];

        NowPlayingNotifier.NotifyPlayingSong(selectedMusic.TrackName);
        Source.PlayOneShot(selectedMusic.Clip, Source.volume);

        float trackLength = selectedMusic.Clip.length;
        StartCoroutine(_scheduleMusicToPlay(trackLength));
    }
}
