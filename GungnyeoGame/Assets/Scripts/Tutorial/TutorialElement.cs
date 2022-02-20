using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialElement : MonoBehaviour
{

    [SerializeField] private GameObject[] sectionObjects;

    public bool hasFinished = false;
    [SerializeField] private float activateCooldown = 4f;

    [SerializeField] private AudioClipHolder clipHolder;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Animator[] animator;

    private int animState = 1;

    private void Awake()
    {
        gameObject.SetActive(false);

        
        foreach (Animator anim in animator)
        {
            if (anim == null)
            {
                Debug.LogError("No animators in " + gameObject + " animator slots. Aborting.");
                return;
            }

            if (anim.GetInteger("currentState") != 0)
            {
                Debug.LogWarning("Check " + anim + " integer state. Should be 0.");
            }
        }
    }

    private void OnEnable()
    {
        ActivateGameObjects(true);
        StartCoroutine(ActivateAnimationOnDelay());
    }

    public void IterateTutorial()
    {
        if (!hasFinished)
        {
            hasFinished = true;
            TutorialManager.instance.ActivateNextSection();
        }
    }

    private void ActivateGameObjects(bool playSound)
    {
        if (playSound) {
        PlaySound();
        }

        foreach (GameObject obj in sectionObjects)
        {
            obj.SetActive(true);
        }
    }
    private void PlaySound()
    {
        if (audioSource) { 
            audioSource.PlayOneShot(clipHolder.GetClip(false)); 
        }
    }
    private void IterateAnimation()
    {
        foreach (Animator anim in animator)
        {
            anim.SetInteger("currentState", animState);
        }
        animState++;
    }

    public void HideSelf()
    {
        foreach (Animator anim in animator)
        {
            anim.SetInteger("currentState", animState);
        }
        animState++;
    }

    private IEnumerator ActivateAnimationOnDelay()
    {
        yield return new WaitForSeconds(activateCooldown);
        IterateAnimation();
    }
    


}
