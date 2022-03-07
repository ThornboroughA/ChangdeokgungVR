using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HingeSound : MonoBehaviour
{

    private Rigidbody rigidBody;
    private XRGrabInteractable grabbable;

    //private bool isActive = false;


    //testing
    [SerializeField] private AudioSource audioSource;



    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        grabbable = GetComponent<XRGrabInteractable>();

        grabbable.selectEntered.AddListener(SelectEnterEventArgs => StartGrab());
    }

    private void StartGrab()
    {
        //isActive = true;

        StartCoroutine(NoisePlayer());
    }


    private IEnumerator NoisePlayer()
    {
        audioSource.Play();

        while (rigidBody.angularVelocity.x > 0.1f || rigidBody.angularVelocity.y > 0.1f || rigidBody.angularVelocity.z > 0.1f)
        {

            float velocity = rigidBody.angularVelocity.x + rigidBody.angularVelocity.y + rigidBody.angularVelocity.z;

            float vol = Mathf.Clamp(0.5f * (velocity + 1f), 0, 1);

            Debug.Log(vol);
            audioSource.volume = vol;

            // play audio based on velocity

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.7f);

        Debug.Log("Stop clip at velocity of " + rigidBody.angularVelocity + ".");
        audioSource.Stop();

        

    }








    /*
    private void OnEnable()
    {
        grabbable = GetComponent<XRGrabInteractable>();

        grabbable.selectEntered.AddListener(SelectEnterEventArgs => ToggleHeld());
        grabbable.selectExited.AddListener(SelectEnterEventArgs => ToggleHeld());
    }
    private void OnDisable()
    {
        grabbable.selectEntered.RemoveListener(SelectEnterEventArgs => ToggleHeld());
        grabbable.selectExited.RemoveListener(SelectEnterEventArgs => ToggleHeld());
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void ToggleHeld()
    {
        isHeld = !isHeld;
    }


    private void Update()
    {
        Debug.Log(rigidBody.angularVelocity);

        //if (!isHeld) return;
        //PlayTestSound();
    }

    private void PlayTestSound()
    {
        while (isHeld)
        {
            audioSource.Play();
            return;
        }
        audioSource.Stop();
    }


    */







}
