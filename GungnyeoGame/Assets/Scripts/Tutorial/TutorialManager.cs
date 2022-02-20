using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [Tooltip("The tutorial elements. They're activated sequentially, so make sure they're in order.")]
    public List<GameObject> tutorialObjects = new List<GameObject>();
    public int tutorialIndex = 0;

    public GameObject[] tutorialSprites;
    
    private Coroutine tutorialRoutine;

    #region Singleton
    public static TutorialManager instance;
    private void Awake()
    {
        if (instance && instance != this)
        {
            Debug.LogWarning("More than one instance of TutorialManager found! Destroying.");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
#endregion


    ////
    ///Need to figure out a method for:
    ///- grab object, then hide
    ///- rotate camera around, then grab object then hide
    ///- teleport, then grab object
    ///- transition scene

    private void Start()
    {
        tutorialRoutine = StartCoroutine(ActivateTutorial());
    }
    private IEnumerator ActivateTutorial()
    {
        yield return new WaitForSeconds(3f);

        tutorialObjects[0].SetActive(true);
    }

    public void ActivateNextSection()
    {
        tutorialIndex++;
        tutorialObjects[tutorialIndex].SetActive(true);
    }

    public void DisableAllSprites()
    {
        foreach (GameObject obj in tutorialSprites)
        {
            obj.SetActive(false);
        }
    }

}
