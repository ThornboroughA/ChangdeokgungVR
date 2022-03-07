using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationMenu : MonoBehaviour
{



    // Index of 0 is English, index of 1 is Korean.
    public void SwitchLanguage(int index)
    {
        Debug.Log("activated");
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

       // SoundCore.instance.PlayFoleySound(SoundCore.SoundType.Wood, 1f, transform);
    }


}
