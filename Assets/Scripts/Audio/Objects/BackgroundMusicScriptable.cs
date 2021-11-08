using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "BackgroundMusic", menuName = "ScriptableObjects/BackgroundMusic")]
public class BackgroundMusicScriptable : ScriptableObject
{
    public BackgroundMusic[] LoopingAudio;
    public BackgroundMusic[] MusicTracks;

    private int _indexOfLastLoopingAudio = -1;
    private int _indexOfLastMusicTrack = -1;

    public BackgroundMusic GetRandomNoRepeat(BackgroundMusicType type)
    {
        if (type == BackgroundMusicType.Loop)
        {
            var result = _getRandomNoRepeat(LoopingAudio, _indexOfLastLoopingAudio);
            _indexOfLastLoopingAudio = result.Item2;
            return result.Item1;
        }
        else if (type == BackgroundMusicType.Song)
        {
            var result = _getRandomNoRepeat(MusicTracks, _indexOfLastMusicTrack);
            _indexOfLastMusicTrack = result.Item2;
            return result.Item1;
        }
        else
        {
            return null;
        }
    }

    private (BackgroundMusic, int) _getRandomNoRepeat(BackgroundMusic[] array, int lastSelected)
    {
        if (array.Length == 1) return (array[0], 0);
        if (array.Length == 0) return (null, -1);

        var validOptions = array.Where((x, i) => i != lastSelected).ToArray();
        int selectedIndex = Random.Range(0, validOptions.Length);
        return (validOptions[selectedIndex], selectedIndex);
    }
}
