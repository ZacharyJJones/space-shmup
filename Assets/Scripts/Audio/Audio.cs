using UnityEngine;

// all of these get loaded in, but are only populated with an AudioClip / AudioSource when it actually gets called.
// when 'cleaning up' list, replace the 'Clip' and 'Source' values with nulls, and dont forget to destroy the audiosource component.

[System.Serializable]
public class Audio
{
  public AudioTrigger Trigger = AudioTrigger.Undefined;
  public AudioSceneTrigger SceneTrigger = AudioSceneTrigger.None;
  public AudioClip Clip;


  [Range(0, 1f)]
  public float Volume = 1f;

  [Range(-3f, 3f)]
  public float Pitch = 1f;

  public bool Loop = false;


  public bool IsGlobal { get; set; }
  public AudioSource Source { get; set; }



  public Audio(AudioTrigger trigger, bool loop, float volume, float pitch)
  {
    Trigger = trigger;
    Loop = loop;
    Volume = volume;
    Pitch = pitch;
  }







  public void Initialize(AudioSource source, bool isGlobal)
  {
    Source = source;
    Source.clip = Clip;

    Source.volume = Volume;
    Source.pitch = Pitch;

    Source.loop = Loop;

    IsGlobal = isGlobal;
  }

  public void Play()
  {
    if (Source == null)
    {
      Debug.LogWarning($"Audio object with trigger '{Trigger}' is not initialized! Audio will not be played.");
      return;
    }

    Source.Play();
  }

  public void Stop()
  {
    if (Source != null)
    {
      Source.Stop();
    }
  }

  public void Cleanup()
  {
    if (Source.isPlaying)
    {
      Source.Stop();
    }

    if (Source != null)
    {
      Object.Destroy(Source);
      Source = null;
    }
  }


  /// <summary> Returns a new <see cref="Audio"/> object which is not initialized -- Only it's ID, Clip, Volume and Pitch are carried across, the rest are defaulted. </summary>
  public Audio Clone()
  {
    var ret = new Audio(Trigger, Loop, Volume, Pitch)
    {
      Clip = this.Clip
    };

    return ret;
  }


  public string ToJson(bool formattedJson = false) => JsonUtility.ToJson(this, formattedJson);
}