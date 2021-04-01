using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundMusic", menuName = "ScriptableObjects/BackgroundMusic")]
public class BackgroundMusicScriptable : ScriptableObject
{
    public AudioClip[] LoopingAudio;
    public AudioClip[] MusicTracks;

    private int _lastLoopingAudio = -1;
    private int _lastMusicTrack = -1;

       

    public AudioClip GetRandom(BackgroundMusicType type, out BackgroundMusicType otherType)
    {
        // set up out var here for simplification
        otherType = BackgroundMusicType.Undefined;


        // check for invalid input
        if (type != BackgroundMusicType.Loop
        &&  type != BackgroundMusicType.Song)
        {
            return null;
        }


        // set up variables to be used
        AudioClip[] array = (type == BackgroundMusicType.Loop) ? LoopingAudio : MusicTracks;
        int lastSelected = (type == BackgroundMusicType.Loop) ? _lastLoopingAudio : _lastMusicTrack;


        // special cases for specific array sizes
        if (array.Length == 1) return array[0];
        else if (array.Length == 0) return null;


        // this loop is used so that the same song won't be played twice in a row
        int selectedIndex = lastSelected;
        while (selectedIndex == lastSelected)
        {
            selectedIndex = Random.Range(0, array.Length);
        }


        // record which song was played, and what the 'other' type is
        if (type == BackgroundMusicType.Loop)
        {
            _lastLoopingAudio = selectedIndex;
            otherType = BackgroundMusicType.Song;
        }
        else
        {
            _lastMusicTrack = selectedIndex;
            otherType = BackgroundMusicType.Loop;
        }

        // return the result!
        return array[selectedIndex];
    }
}
