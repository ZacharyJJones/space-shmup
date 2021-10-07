using System.Collections;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    // singleton pattern
    public static BackgroundMusicManager Instance;
    public static bool InstanceExists = (Instance != null);


    [Range(0, 1)]
    public float MusicVolume;

    public BackgroundMusicScriptable BackgroundMusic;

    public NowPlayingNotifier NowPlayingNotifier;

    private AudioSource _loopMusicSource;
    private AudioSource _songMusicSource;


    // Start is called before the first frame update
    void Start()
    {
        if (InstanceExists)
        {
            // Need to update the "now playing notifier"
            Instance.NowPlayingNotifier = this.NowPlayingNotifier;

            // get rid of this non-singleton
            Destroy(gameObject);
            return;
        }
        else
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
    }



    IEnumerator _scheduleMusicToPlay(float timeUntilStart)
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
        NowPlayingNotifier.ShowPanel(clip.name);
        source.PlayOneShot(clip, MusicVolume);
        return clip.length;
    }
}
