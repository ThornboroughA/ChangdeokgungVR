using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooling))]
public class SoundCore : MonoBehaviour
{


    public enum SoundType { Wood, Ceramic, Paper, Metal, MetalSmall };

    [Header("Foley Sound Lists")]
    [SerializeField] private AudioClipHolder woodList; 
    [SerializeField] private AudioClipHolder ceramicList;
    [SerializeField] private AudioClipHolder paperList;
    [SerializeField] private AudioClipHolder metalList;
    [SerializeField] private AudioClipHolder metalSmallList;


    [Header("Other")]
    [Range(0, 1)]
    [SerializeField] private float volumeAdjustment = 1f;
    
    #region Singleton
    public static SoundCore instance;
    private void Awake()
    {
        if (instance && instance != this)
        {
            Debug.LogWarning("More than one instance of SoundCore found! Destroying.");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    
    public void PlayFoleySound(SoundType soundType, float volume, Transform location)
    {
        GameObject foleyPlayer = ObjectPooling.instance.GetPooledObject();
        if (foleyPlayer == null) return;
        AudioSource foleyAudioSource = foleyPlayer.GetComponent<AudioSource>();
        foleyAudioSource.spatialBlend = 1f;

        foleyPlayer.transform.position = location.position;
        foleyPlayer.SetActive(true);
        foleyAudioSource.volume = volume * volumeAdjustment;
        foleyAudioSource.PlayOneShot(GetClipOfType(soundType));

        StartCoroutine(ObjectDeactivateOnDelay(foleyPlayer));
    }
    private IEnumerator ObjectDeactivateOnDelay(GameObject obj)
    {
        yield return new WaitForSeconds(5f);
        obj.SetActive(false);
    }


    private AudioClip GetClipOfType(SoundType soundType)
    {
        AudioClip clip;

        switch (soundType)
        {
            case SoundType.Wood:
                clip = woodList.GetClip(false);
                break;
            case SoundType.Ceramic:
                clip = ceramicList.GetClip(false);
                break;
            case SoundType.Paper:
                clip = paperList.GetClip(false);
                break;
            case SoundType.Metal:
                clip = metalList.GetClip(false);
                break;
            case SoundType.MetalSmall:
                clip = metalSmallList.GetClip(false);
                break;
            default:
                Debug.LogWarning("Incorrect soundType in " + soundType);
                clip = null;
                break;
        }

        return clip;
    }



}
