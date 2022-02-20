using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{

    [SerializeField] private bool hideHandOnGrab = false;
    private SkinnedMeshRenderer mesh;

    //ANIMATION
    [SerializeField] private float animationSpeed;

    private Animator animator;
    private float triggerTarget;
    private float gripTarget;
    private float gripCurrent;
    private float triggerCurrent;


    private const string AnimatorGripParam = "Grip";
    private const string AnimatorTriggerParam = "Trigger";
    private static readonly int Grip = Animator.StringToHash(AnimatorGripParam);
    private static readonly int Trigger = Animator.StringToHash(AnimatorTriggerParam);

    
    private void Start()
    {
        
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        
        //ANIMATION
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        AnimateHand();
    }

    internal void SetGrip(float v)
    {
        //gets the value passed from HandController
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    private void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(Grip, gripCurrent);

        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(Trigger, triggerCurrent);

        }
    }

    public void ToggleVisibility()
    {
        //based on an event, currentrly in the XR Ray Interactor
        if (!hideHandOnGrab)
            return;

        mesh.enabled = !mesh.enabled;

    }
}