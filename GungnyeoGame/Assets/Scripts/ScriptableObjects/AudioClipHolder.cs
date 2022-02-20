using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoundsList", menuName = "ScriptableObjects/SoundsList")]
public class AudioClipHolder : ScriptableObject
{

    public AudioClip[] audioClips;
    private int playIndex = 0;

    public AudioClip GetClip(bool isRandom)
    {
        AudioClip clip;

        if (isRandom)
        {
            clip = audioClips[Random.Range(0, audioClips.Length)];
        }
        else
        {
            clip = audioClips[playIndex];
            playIndex = (playIndex + 1) % audioClips.Length;
        }

        return clip;
    }
 

}
