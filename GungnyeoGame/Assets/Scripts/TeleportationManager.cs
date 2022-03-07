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
    [SerializeField] private List<TeleportAnchorSubject> activeSubjects = new List<TeleportAnchorSubject>();


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
        #region Controllers 1
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
        #endregion

        //Hide anchors at start
        foreach (TeleportAnchorSubject tp in teleportationAnchors)
        {
            tp.MakeInvisible();
        }
        foreach (TeleportAnchorSubject anchor in teleportationAnchors)
        {
            if (anchor.visibleTeleports.Length == 0) Debug.LogWarning("No teleports assigned to " + anchor.gameObject);
        }
    }

    #region Controllers 2
    private void OnTeleportActivateLeft(InputAction.CallbackContext context)
    {
        ToggleRaycasts(0, false);
        RevealLocalAnchors();
    }
    private void OnTeleportCancelLeft(InputAction.CallbackContext context)
    {
        ToggleRaycasts(0, true);
        HideLocalAnchors();
    }
    private void OnTeleportActivateRight(InputAction.CallbackContext context)
    {
        ToggleRaycasts(1, false);
        RevealLocalAnchors();
    }
    private void OnTeleportCancelRight(InputAction.CallbackContext context)
    {
        ToggleRaycasts(1, true);
        HideLocalAnchors();
    }
    #endregion

    public void ResetTeleports()
    { 
        ToggleRaycasts(true);
        ClearLocalAnchors();
        
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

    public void SetLocalAnchors(TeleportAnchorSubject currentSubject)
    {
        foreach(TeleportAnchorSubject localSubject in currentSubject.visibleTeleports)
        {
            activeSubjects.Add(localSubject);
        }
    }
    public void ClearLocalAnchors()
    {
        foreach (TeleportAnchorSubject active in activeSubjects)
        {
            active.MakeInvisible();
        }
        activeSubjects.Clear();
    }
    public void RevealLocalAnchors()
    {
        foreach (TeleportAnchorSubject active in activeSubjects)
        {
            active.MakeVisible();
        }
    }
    public void HideLocalAnchors()
    {
        foreach (TeleportAnchorSubject active in activeSubjects)
        {
            active.MakeInvisible();
        }
    }
}
