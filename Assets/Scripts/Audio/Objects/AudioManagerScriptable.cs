using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioManagerData", menuName = "ScriptableObjects/AudioManager")]
public class AudioManagerScriptable : ScriptableObject
{
    public Audio[] Audio;

    // By convention, data from a scriptable object should not be directly used.
    public Audio[] GetClone()
    {
        if (Audio == null)
            return new Audio[0];

        return Audio.Select(x => x.Clone()).ToArray();
    }
}
