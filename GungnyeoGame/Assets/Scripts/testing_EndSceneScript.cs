using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing_EndSceneScript : MonoBehaviour
{

    public void RestartScene()
    {
        GameManager.instance.NextScene("DaejojeonTesting");
    }


}
