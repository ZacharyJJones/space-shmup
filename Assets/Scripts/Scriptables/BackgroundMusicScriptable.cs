using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "BackgroundMusic", menuName = "ScriptableObjects/BackgroundMusic")]
public class BackgroundMusicScriptable : ScriptableObject
{
    public AudioClip[] LoopingAudio;
    public AudioClip[] MusicTracks;

    private int _indexOfLastLoopingAudio = -1;
    private int _indexOfLastMusicTrack = -1;

    public AudioClip GetRandomNoRepeat(BackgroundMusicType type)
    {
        if (type == BackgroundMusicType.Loop)
        {
            return _getRandomNoRepeat(LoopingAudio, _indexOfLastLoopingAudio);
        }
        else if (type == BackgroundMusicType.Song)
        {
            return _getRandomNoRepeat(MusicTracks, _indexOfLastMusicTrack);
        }
        else
        {
            return null;
        }
    }

    private AudioClip _getRandomNoRepeat(AudioClip[] array, int lastSelected)
    {
        if (array.Length == 1)
        {
            return array[0];
        }
        if (array.Length == 0)
        {
            return null;
        }

        var validOptions = array.Where((x, i) => i != lastSelected).ToArray();
        return validOptions[Random.Range(0, validOptions.Length)];
    }
}
