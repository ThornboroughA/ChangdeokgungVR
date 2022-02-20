using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    ActionBasedController controller;

    
    [SerializeField] private Hand hand;
    [SerializeField] private GameObject rayObject;


    private void Awake()
    {
        controller = GetComponent<ActionBasedController>();
    }

    private void Start()
    {
        //Hide rays at start
        ToggleRay(true);
    }

    private void Update()
    {

        hand.SetGrip(controller.selectAction.action.ReadValue<float>());
        hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }

    public void ToggleRay()
    {
        rayObject.SetActive(!rayObject.activeSelf);
    }
    public void ToggleRay(bool deactivate)
    {
        if (deactivate)
        {
            rayObject.SetActive(false);
        }
        if (!deactivate)
        {
            rayObject.SetActive(true);
        }
    }

}
