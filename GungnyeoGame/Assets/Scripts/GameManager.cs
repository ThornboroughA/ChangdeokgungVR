using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;

public class GameManager : MonoBehaviour
{
    


    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance && instance != this)
        {
            Debug.LogWarning("More than one instance of GameManager found! Destroying.");
            Destroy(this.gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion


    private void Start()
    {
        StartCoroutine(SwitchToKorean());
    }


    private IEnumerator SwitchToKorean()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("korean switch");
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
    }

    public void NextScene(string sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }




}
