using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public class TeleportAnchorSubject : TeleportationAnchor
    {

        public TeleportAnchorSubject[] visibleTeleports;

        [SerializeField] private GameObject textObject;

        [SerializeField] private bool hasVisited = false;

        private void Start()
        {
            textObject.SetActive(false);
        }

        public void MakeVisible()
        {
            RevealSplash();

            gameObject.SetActive(true);

            if (hasVisited) textObject.SetActive(true);

        }
        public void MakeInvisible()
        {
            gameObject.SetActive(false);
        }

        public void SetVisited()
        {
            hasVisited = true;
        }

        private void RevealSplash()
        {
            //add later
        }

        





    }
}
