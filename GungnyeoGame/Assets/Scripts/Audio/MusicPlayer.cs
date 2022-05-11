using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicTracks;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void JungjeonMusic()
    {
        if (audioSource.clip == musicTracks[0]) return;

        audioSource.Stop();
        audioSource.clip = musicTracks[0];
        audioSource.Play();
    }
    public void GungnyeoMusic()
    {
        if (audioSource.clip == musicTracks[1]) return;
        
        audioSource.Stop();
        audioSource.clip = musicTracks[1];
        audioSource.Play();
    }

    private IEnumerator StartFade(float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }



}
