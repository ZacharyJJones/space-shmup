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


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(this.gameObject);

            // set up audio sources to play audio from
            _loopMusicSource = gameObject.AddComponent<AudioSource>();
            _songMusicSource = gameObject.AddComponent<AudioSource>();

            // start off with a looping track.
            StartCoroutine(_scheduleMusicToPlay(0f));
        }
        else
        {
            // Only relevant info is the scene-specific NowPlayingNotifier script.
            Instance.NowPlayingNotifier = this.NowPlayingNotifier;
            Destroy(gameObject);
        }
    }

    private IEnumerator _scheduleMusicToPlay(float timeUntilStart)
    {
        while (timeUntilStart >= 0)
        {
            timeUntilStart -= Time.deltaTime;
            yield return null;
        }

        var clip = BackgroundMusic.GetRandomNoRepeat(BackgroundMusicType.Song);
        float timeOfTrack = _playTrack(_songMusicSource, clip);
        StartCoroutine(_scheduleMusicToPlay(timeOfTrack));
    }

    private float _playTrack(AudioSource source, AudioClip clip)
    {
        NowPlayingNotifier.NotifyPlayingSong(clip.name);
        source.PlayOneShot(clip, MusicVolume);
        return clip.length;
    }
}
