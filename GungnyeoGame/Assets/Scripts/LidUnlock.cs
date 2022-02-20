using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LidUnlock : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float limitsMin = -115f;
    [SerializeField] private Vector3 axis = new Vector3(1f, 0f, 0f);


    public void UnlockSelf()
    {
        AddPhysicsComponents();
        EnableXR();
    }

    private void AddPhysicsComponents()
    {
        rb.useGravity = true;
        rb.isKinematic = false;

        HingeJoint hinge;
        hinge = gameObject.AddComponent<HingeJoint>();

        hinge.axis = axis;
        hinge.anchor = Vector3.zero;
        
        hinge.useSpring = true;
        JointSpring spring = hinge.spring;
        spring.damper = 0.5f;
        hinge.spring = spring;

        hinge.useLimits = true;
        JointLimits limits = hinge.limits;
        limits.min = limitsMin;
        hinge.limits = limits;
    }
    private void EnableXR()
    {
        GetComponent<XRGrabInteractable>().enabled = true;
    }


}
