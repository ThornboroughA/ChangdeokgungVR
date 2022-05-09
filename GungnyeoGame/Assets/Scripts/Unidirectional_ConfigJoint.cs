using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidirectional_ConfigJoint : MonoBehaviour
{

    [SerializeField] private float motionDistance = 0.5f;
    [Tooltip("Locks the motion of the corresponding axis. 0 is locked, 1 is limited.")]
    [SerializeField] private Vector3 axisLock = new Vector3(0, 0, 0);
    [Tooltip("Does this GameObject have children with rigidbodies? All children must have rigidbodies.")]
    [SerializeField] private bool hasRBChild = false;

    private ConfigurableJoint configJoint;

    [Tooltip("Offset for the drawer's linear motion. If it has children, it seems to have a different offset otherwise.")]
    [SerializeField] private float anchorDistance;
    

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        TransformObjectInDirection(false);

        StartCoroutine(Delay());
    }

    private void TransformObjectInDirection(bool positive)
    {
        float motionHalf = motionDistance * 0.5f;

        Vector3 offset = new Vector3(motionHalf * axisLock.x, motionHalf * axisLock.y, motionHalf * axisLock.z);
        Vector3 newPosition;

        if (positive) { 
            newPosition = rigidBody.position + transform.TransformDirection(offset);
        } else {
            newPosition = rigidBody.position + transform.TransformDirection(-offset);
        }
        rigidBody.position = newPosition;
    }
    private void AddConfigurableJoint ()
    {
        configJoint = gameObject.AddComponent<ConfigurableJoint>();

        SoftJointLimit limit = new SoftJointLimit();
        limit.limit = motionDistance * 0.5f ;
        configJoint.linearLimit = limit;
        

        LockAxis(configJoint);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.25f);
        AddConfigurableJoint();
        yield return new WaitForSeconds(0.5f);

        // 34.77 2.97 -0.31

        if (hasRBChild == true)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
            }

            Vector3 anchorValues = configJoint.connectedAnchor;
            anchorValues = Vector3.Scale(anchorValues, (new Vector3(1, 1, 1) - axisLock));

            configJoint.autoConfigureConnectedAnchor = false;
            configJoint.connectedAnchor = anchorValues - (anchorDistance * axisLock);
        }

        TransformObjectInDirection(true);
    }

    private void LockAxis(ConfigurableJoint configJoint)
    {
        // Lock linear values.
        if (axisLock.x == 0)
        {
            configJoint.xMotion = ConfigurableJointMotion.Locked;
        }
        else
        {
            configJoint.xMotion = ConfigurableJointMotion.Limited;
        }
        if (axisLock.y == 0)
        {
            configJoint.yMotion = ConfigurableJointMotion.Locked;
        }
        else
        {
            configJoint.yMotion = ConfigurableJointMotion.Limited;
        }
        if (axisLock.z == 0)
        {
            configJoint.zMotion = ConfigurableJointMotion.Locked;
        }
        else
        {
            configJoint.zMotion = ConfigurableJointMotion.Limited;
        }
        // Lock angular values.

        configJoint.angularXMotion = ConfigurableJointMotion.Locked;
        configJoint.angularYMotion = ConfigurableJointMotion.Locked;
        configJoint.angularZMotion = ConfigurableJointMotion.Locked;
    }








}
