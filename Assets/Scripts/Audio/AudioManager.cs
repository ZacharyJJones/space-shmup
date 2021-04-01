using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public static bool InstanceExists => (Instance != null);




    // 'singleton' stuff
    public AudioManagerScriptable GlobalAudio;
    public AudioManagerScriptable ThisSceneAudio { get; set; }

    public Dictionary<AudioTrigger, Audio> AudioByTrigger { get; set; }

    private bool _isSingletonInstance { get; set; } = false;
    private List<AudioTrigger> _sceneEndAudioTriggers { get; set; }


    // 'local' stuff
    public AudioManagerScriptable SceneAudio;






    void Start()
    {
        AudioByTrigger = new Dictionary<AudioTrigger, Audio> { };

        // only the singleton distance actually handles playing the audio. The other instances simply provide it with more data to use.
        if (InstanceExists)
        {
            SceneManager.sceneLoaded += _onSceneLoad;
        }

        _onStartSetup();
    }

    void OnDestroy()
    {
        // only the singleton distance actually handles playing the audio. The other instances simply provide it with more data to use.
        if (_isSingletonInstance)
        {
            SceneManager.sceneLoaded -= _onSceneLoad;
        }
    }




    public Audio GetAudio(AudioTrigger trigger)
    {
        AudioByTrigger.TryGetValue(trigger, out var audio);
        return audio;
    }


    public void PlayTriggers(IEnumerable<AudioTrigger> triggers)
    {
        foreach (AudioTrigger trigger in triggers)
        {
            PlayTrigger(trigger);
        }
    }

    public void PlayTrigger(AudioTrigger trigger)
    {
        if (!AudioByTrigger.TryGetValue(trigger, out var audio))
        {
            return;
        }

        audio.Play();
    }

    public void LoadAudio(AudioManagerScriptable audioScriptable, bool isGlobal = false)
    {
        if (audioScriptable == null)
        {
            return;
        }

        var cloneAudio = audioScriptable.GetClone();
        foreach (Audio audio in cloneAudio)
        {
            var source = gameObject.AddComponent<AudioSource>();
            audio.Initialize(source, isGlobal);

            if (!AudioByTrigger.ContainsKey(audio.Trigger))
            {
                AudioByTrigger.Add(audio.Trigger, audio);
            }
        }
    }

    public void Editor_Reload()
    {
        // 1. clear out AudioByID
        Instance.AudioByTrigger = new Dictionary<AudioTrigger, Audio> { };

        // 2. load global audio
        LoadAudio(Instance.GlobalAudio);

        // 3. load 'thisScene' audio
        LoadAudio(Instance.ThisSceneAudio);
    }





    private void _onStartSetup()
    {
        if (!InstanceExists)
        {
            // set this up as the singleton instance
            Instance = this;
            _isSingletonInstance = true;
            transform.SetParent(null);
            DontDestroyOnLoad(this.gameObject);
            LoadAudio(GlobalAudio, true);


            // this only gets used by the singleton
            _sceneEndAudioTriggers = new List<AudioTrigger> { };
        }


        // nulls are not an issue
        Instance.LoadAudio(SceneAudio);
        Instance.LoadAudio(SceneAudio.ChildAudio);
        Instance.ThisSceneAudio = SceneAudio;


        // play & set up scene start & end audio
        foreach (var audio in SceneAudio.Audio)
        {
            if (_sceneTriggerPlaysOnStart(audio.SceneTrigger))
            {
                Instance.PlayTrigger(audio.Trigger);
            }

            if (_sceneTriggerPlaysOnEnd(audio.SceneTrigger))
            {
                Instance._sceneEndAudioTriggers.Add(audio.Trigger);
            }
        }


        // singleton -- only one gets to exist
        if (InstanceExists)
        {
            Destroy(this);
        }
    }
    private bool _sceneTriggerPlaysOnStart(AudioSceneTrigger trigger)
    {
        if (trigger == AudioSceneTrigger.SceneStart)
        {
            return true;
        }
        else if (trigger == AudioSceneTrigger.SceneStartAndEnd)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool _sceneTriggerPlaysOnEnd(AudioSceneTrigger trigger)
    {
        if (trigger == AudioSceneTrigger.SceneEnd)
        {
            return true;
        }
        else if (trigger == AudioSceneTrigger.SceneStartAndEnd)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    private void _onSceneLoad(Scene scene, LoadSceneMode loadMode)
    {
        _playSceneEndTriggers();
        _removeOldSceneAudio();
    }


    private void _playSceneEndTriggers()
    {
        PlayTriggers(_sceneEndAudioTriggers);
        _sceneEndAudioTriggers.Clear();
    }


    private void _removeOldSceneAudio()
    {
        var newDict = new Dictionary<AudioTrigger, Audio> { };

        foreach (KeyValuePair<AudioTrigger, Audio> pair in AudioByTrigger)
        {
            if (!pair.Value.IsGlobal)
            {
                pair.Value.Cleanup();
                continue;
            }

            newDict.Add(pair.Value.Trigger, pair.Value);
        }

        AudioByTrigger = newDict;
    }




}
