using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class LockBehavior : MonoBehaviour
{
    public GameObject key;

    [SerializeField] private GameObject lockInside;
    [SerializeField] private LidUnlock lidScript;

    [SerializeField] private Rigidbody objRigid;
    [SerializeField] private XRGrabInteractable grabInteractable;

    [Tooltip("Name of the animation clip for the lock opening.")]
    [SerializeField] private string animationStringName = "Base Layer.LockOpen";

    private void Start()
    {
        grabInteractable.enabled = false;
    }

    public void KeyIsIn()
    {
        PlayAnimation();
        DisableKeyAndSocket();
        ActivateLock();
    }

    private void ActivateLock()
    {
        grabInteractable.enabled = true;
    }

    public void Unlock()
    {
        objRigid.isKinematic = false;
        objRigid.useGravity = true;
        lidScript.UnlockSelf();

        //gameObject.layer = 0;

        //switch colliders; spherecollider is for grabbing, boxcollider is for the object's physics
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<BoxCollider>().enabled = true;
        
    }


    private void PlayAnimation()
    {
        lockInside.GetComponent<Animator>().Play(animationStringName);
    }


    private void DisableKeyAndSocket()
    {
        //key.GetComponent<BoxCollider>().enabled = false;
        key.layer = 0;
    }

}
