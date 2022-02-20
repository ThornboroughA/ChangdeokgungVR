using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public class TeleportAnchorSubject : TeleportationAnchor
    {


        public void MakeVisible()
        {
            RevealSplash();

            gameObject.SetActive(true);

        }
        public void MakeInvisible()
        {
            gameObject.SetActive(false);
            //StartCoroutine(SetInactive(0.35f));
        }


        private void RevealSplash()
        {
            //add later
        }


        private IEnumerator SetInactive(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }


    }
}
