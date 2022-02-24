using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// A class to attach to an interactive game object and have it play a sound. 
/// Requires SoundCore in the scene.
/// </summary>

[RequireComponent(typeof(XRGrabInteractable))]
public class FoleySound : MonoBehaviour
{

    [SerializeField] private SoundCore.SoundType soundType;
    private Rigidbody rigidBody;
    XRGrabInteractable grabbable;

    private bool onCooldown = false;

    private void OnEnable()
    {
        grabbable = GetComponent<XRGrabInteractable>();

        grabbable.selectEntered.AddListener( SelectEnterEventArgs => ActivateSound() );
    }
    private void OnDisable()
    {
        grabbable.selectEntered.RemoveListener(SelectEnterEventArgs => ActivateSound());
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Made for hand grabbing.
    public void ActivateSound()
    {
        if (onCooldown)
            return;

        SoundCore.instance.PlayFoleySound(soundType, 0.5f, transform);

        StartCoroutine(Cooldown(0.2f));
    }
    // Made for rigidbody sounds.
    public void ActivateSound(float audioImpact)
    {
        if (onCooldown)
            return;

        SoundCore.instance.PlayFoleySound(soundType, audioImpact, transform);

        StartCoroutine(Cooldown(0.05f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Collision layer is not hands.
        if (collision.gameObject.layer == 8)
            return;

        // Normalizing values for the audio volume (0 - 1) based on velocities of up to 2.5.
        float audioImpact = Mathf.Clamp((rigidBody.velocity.magnitude / 3f), 0.2f, 1);

        ActivateSound(audioImpact);
    }

    private IEnumerator Cooldown(float timer)
    {
        onCooldown = true;
        yield return new WaitForSeconds(timer);
        onCooldown = false;
    }




}
