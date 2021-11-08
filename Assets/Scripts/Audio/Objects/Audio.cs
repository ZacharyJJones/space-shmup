using UnityEngine;

[System.Serializable]
public class Audio
{
    // Editor Fields
    public AudioTrigger Trigger = AudioTrigger.Undefined;
    public AudioClip Clip;
    [Range(0, 1f)]
    public float Volume = 1f;
    [Range(-3f, 3f)]
    public float Pitch = 1f;
    public bool Loop = false;

    // Runtime Fields
    public bool IsGlobal { get; set; }
    public AudioSource Source { get; set; }

    
    public void Play() => Source.Play();
    public void Stop() => Source.Stop();

    public void Initialize(AudioSource source, bool isGlobal)
    {
        Source = source;
        Source.clip = Clip;

        Source.volume = Volume;
        Source.pitch = Pitch;

        Source.loop = Loop;

        IsGlobal = isGlobal;
    }

    public void Cleanup()
    {
        if (Source.isPlaying)
            Source.Stop();

        Object.Destroy(Source);
        Source = null;
    }

    public Audio Clone()
    {
        var ret = new Audio()
        {
            Trigger = this.Trigger,
            Loop = this.Loop,
            Volume = this.Volume,
            Pitch = this.Pitch,
            Clip = this.Clip
        };

        return ret;
    }
}
