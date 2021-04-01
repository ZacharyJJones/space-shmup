using UnityEngine;

[CreateAssetMenu(fileName = "AudioManagerData", menuName = "ScriptableObjects/AudioManager")]
public class AudioManagerScriptable : ScriptableObject
{
    // Useful for when a scene also uses a bunch of audio from another scene. This (should) get loaded in alongside this scriptable object.
    // Should not be used recursively.
    public AudioManagerScriptable ChildAudio;
    
    // preset data is what gets copied -- should not be edited by computer since this is a scriptable object.
    public Audio[] Audio;



    // cloned data is used by the computer
    public Audio[] GetClone()
    {
        var ret = new Audio[Audio.Length];

        for (int i = 0; i < Audio.Length; i++)
        {
            ret[i] = Audio[i].Clone();
        }

        return ret;
    }

}
