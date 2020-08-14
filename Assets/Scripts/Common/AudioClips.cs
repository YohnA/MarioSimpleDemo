using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClips")]
public class AudioClips : ScriptableObject
{
    public List<AudioClipWithName> Clips = new List<AudioClipWithName>();

    public AudioClip GetClip(string name)
    {
        return Clips.Find(a => { return a.name == name; }).clip;
    }
}
[System.Serializable]
public class AudioClipWithName
{
    public string name;
    public AudioClip clip;
}