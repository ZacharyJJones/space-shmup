using System;
using System.Collections;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    // Editor Fields
    private static BackgroundMusicManager Instance;
    [Range(0, 1)]
    public float MusicVolume;
    public BackgroundMusicScriptable BackgroundMusic;
    public NowPlayingNotifier NowPlayingNotifier;

    // Runtime Fields
    private AudioSource _loopMusicSource;
    private AudioSource _songMusicSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(this.gameObject);

            // set up audio sources to play audio from
            _loopMusicSource = gameObject.AddComponent<AudioSource>();
            _songMusicSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            // Only relevant info is the scene-specific NowPlayingNotifier script.
            Instance.NowPlayingNotifier = this.NowPlayingNotifier;
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // start off with a looping track.
        StartCoroutine(_scheduleMusicToPlay(0f));
    }

    private IEnumerator _scheduleMusicToPlay(float timeUntilStart)
    {
        while (timeUntilStart >= 0)
        {
            timeUntilStart -= Time.deltaTime;
            yield return null;
        }

        BackgroundMusic music = BackgroundMusic.GetRandomNoRepeat(BackgroundMusicType.Song);
        float timeOfTrack = _playTrack(_songMusicSource, music);
        StartCoroutine(_scheduleMusicToPlay(timeOfTrack));
    }

    private float _playTrack(AudioSource source, BackgroundMusic music)
    {
        NowPlayingNotifier.NotifyPlayingSong(music.TrackName);
        source.PlayOneShot(music.Clip, MusicVolume);
        return music.Clip.length;
    }
}
