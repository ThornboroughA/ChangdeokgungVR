using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class TeleportationManager : MonoBehaviour
{

    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private HandController[] handControllers;

    [SerializeField] private TeleportAnchorSubject[] teleportationAnchors;
    [SerializeField] private Transform playerLocation;
    [SerializeField] private List<TeleportAnchorSubject> activeSubjects = new List<TeleportAnchorSubject>();

    [SerializeField] private float teleportMaxRange = 10f, teleportMinRange = 3f;

    private Coroutine cooldownTimer;
    private bool cooldownState = false;

    [SerializeField] private bool useScript = true;

    #region Singleton
    public static TeleportationManager instance;
    private void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("More than one instance of TeleportationManager found!");
            return;
        }
        instance = this;

    }
    #endregion

    private void Start()
    {   
        

        //Subscribe to the input actions
        var activateLeft = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        activateLeft.Enable();
        activateLeft.performed += OnTeleportActivateLeft;

        var cancelLeft = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Cancel");
        cancelLeft.Enable();
        cancelLeft.performed += OnTeleportCancelLeft;

        var activateRight = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Activate");
        activateRight.Enable();
        activateRight.performed += OnTeleportActivateRight;

        var cancelRight = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Cancel");
        cancelRight.Enable();
        cancelRight.performed += OnTeleportCancelRight;

        if (useScript == false)
        {
            return;
        }

        //Hide anchors at start
        foreach (TeleportAnchorSubject tp in teleportationAnchors)
        {
            tp.MakeInvisible();
        }
        

    }

    private void OnTeleportActivateLeft(InputAction.CallbackContext context)
    {
        ToggleRaycasts(0, false);
        RevealAnchors();
    }
    private void OnTeleportCancelLeft(InputAction.CallbackContext context)
    {
        ToggleRaycasts(0, true);
        HideAnchors();
    }
    private void OnTeleportActivateRight(InputAction.CallbackContext context)
    {
        ToggleRaycasts(1, false);
        RevealAnchors();
    }
    private void OnTeleportCancelRight(InputAction.CallbackContext context)
    {
        ToggleRaycasts(1, true);
        HideAnchors();
    }

    public void ResetTeleports()
    { 
        if (cooldownState)
            return;

        ToggleRaycasts(true);
        HideAnchors();
        cooldownTimer = StartCoroutine(CooldownTimer(1f));
        
    }


    private void ToggleRaycasts(bool deactivate)
    { //fully deactivate
        foreach(HandController conts in handControllers)
        {
            conts.ToggleRay(true);
        }
    }
    private void ToggleRaycasts(int conID, bool rayState)
    { //toggle
        
        handControllers[conID].ToggleRay(rayState);
        
    }

    //on press action
    public void RevealAnchors()
    {
        foreach(TeleportAnchorSubject tpSub in teleportationAnchors)
        {
            float range = GetRange(tpSub.gameObject.transform);
            if (range < teleportMaxRange && range > teleportMinRange)
            {
                tpSub.MakeVisible();
                activeSubjects.Add(tpSub);
            }
        }
    }

    //on release action
    public void HideAnchors()
    {
        //makes all revealed TP anchors invisible
        foreach (TeleportAnchorSubject active in activeSubjects)
        {
            active.MakeInvisible();
        }
        activeSubjects.Clear();
        
    }

    private float GetRange(Transform subTransform)
    {
        float dist = Vector3.Distance(subTransform.position, playerLocation.position);
        return dist;
    }


    private IEnumerator CooldownTimer(float time)
    {
        cooldownState = true;
        yield return new WaitForSeconds(time);
        cooldownState = false;
    }
}
