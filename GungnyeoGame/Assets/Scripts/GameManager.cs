using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    public void NextScene(string sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }




}
