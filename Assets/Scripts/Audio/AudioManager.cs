using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private bool _isSingletonInstance { get; set; } = false;

    [SerializeField]
    private AudioManagerScriptable GlobalAudio;
    [SerializeField]
    private AudioManagerScriptable SceneAudio;

    private Dictionary<AudioTrigger, Audio> _audioByTrigger { get; set; }


    private void Start()
    {
        // First-Instance Setup
        if (Instance == null)
        {
            // set up as singleton
            Instance = this;
            _isSingletonInstance = true;
            SceneManager.sceneLoaded += _cleanupAudioDict;

            _audioByTrigger = new Dictionary<AudioTrigger, Audio> { };

            transform.SetParent(null);
            DontDestroyOnLoad(this.gameObject);

            // Only the singleton instantiation loads shared audio, because
            // ... "shared audio" is the same data between all managers.
            if (GlobalAudio)
                _loadAudio(GlobalAudio.GetClone(), true);
        }

        // Always load the current scene's audio into the singleton
        if (SceneAudio)
            Instance._loadAudio(SceneAudio.GetClone());

        if (!_isSingletonInstance)
            Destroy(this);
    }

    private void OnDestroy()
    {
        if (_isSingletonInstance)
        {
            SceneManager.sceneLoaded -= _cleanupAudioDict;
        }
    }

    public void PlayTrigger(AudioTrigger trigger)
    {
        _audioByTrigger[trigger].Play();
    }

    private void _loadAudio(IEnumerable<Audio> audios, bool isSharedAudio = false)
    {
        foreach (var triggeredAudio in audios)
        {
            if (_audioByTrigger.ContainsKey(triggeredAudio.Trigger))
                continue;

            var source = gameObject.AddComponent<AudioSource>();
            triggeredAudio.Initialize(source, isSharedAudio);

            if (triggeredAudio.Trigger == AudioTrigger.Scene_Start)
            {
                triggeredAudio.Play();
                Destroy(source, triggeredAudio.Clip.length);
            }
            else
            {
                _audioByTrigger.Add(triggeredAudio.Trigger, triggeredAudio);
            }
        }
    }

    private void _cleanupAudioDict(Scene _discard1, LoadSceneMode _discard2)
    {
        var newDict = new Dictionary<AudioTrigger, Audio> { };
        foreach (var pair in _audioByTrigger)
        {
            if (pair.Value.IsGlobal)
            {
                newDict.Add(pair.Value.Trigger, pair.Value);
            }
            else
            {
                pair.Value.Cleanup();
            }
        }

        _audioByTrigger = newDict;
    }
}
