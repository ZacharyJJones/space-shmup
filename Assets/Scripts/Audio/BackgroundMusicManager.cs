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
            // update the "NowPlayingNotifier"
            Instance.NowPlayingNotifier = this.NowPlayingNotifier;

            // get rid of this non-singleton
            Destroy(gameObject);
            return;
        }

        // since no instance exists yet, this one gets set up as the instance.
        Instance = this;
        transform.SetParent(null); 
        DontDestroyOnLoad(this.gameObject);


        // set up audio sources to play audio from
        _loopMusicSource = gameObject.AddComponent<AudioSource>();
        _songMusicSource = gameObject.AddComponent<AudioSource>();

        // start off with a looping track.
        StartCoroutine(_scheduleMusicToPlay(0f, BackgroundMusicType.Loop));
    }



    IEnumerator _scheduleMusicToPlay(float timeUntilStart, BackgroundMusicType musicType)
    {
        while (timeUntilStart >= 0)
        {
            timeUntilStart -= Time.deltaTime;
            yield return null;
        }

        // play the scheduled loop music, and schedule song music to play afterwards
        var clip = BackgroundMusic.GetRandom(musicType, out var otherType);
        var source = (musicType == BackgroundMusicType.Loop) ? _loopMusicSource : _songMusicSource;
        float timeOfTrack = _playTrack(source, clip);
        StartCoroutine(_scheduleMusicToPlay(timeOfTrack, otherType));
    }


    private float _playTrack(AudioSource source, AudioClip clip)
    {
        NowPlayingNotifier.ShowPanel(clip.name);
        source.PlayOneShot(clip, MusicVolume);
        return clip.length;
    }

}
