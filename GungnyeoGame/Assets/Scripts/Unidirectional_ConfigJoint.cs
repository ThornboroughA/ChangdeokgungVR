using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidirectional_ConfigJoint : MonoBehaviour
{

    [SerializeField] private float motionDistance = 0.5f;
    [Tooltip("Locks the motion of the corresponding axis. 0 is locked, 1 is limited.")]
    [SerializeField] private Vector3 axisLock = new Vector3(0, 0, 0);

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
        ConfigurableJoint configJoint = gameObject.AddComponent<ConfigurableJoint>();

        SoftJointLimit limit = new SoftJointLimit();
        limit.limit = motionDistance * 0.5f ;
        configJoint.linearLimit = limit;

        LockAxis(configJoint);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.25f);
        AddConfigurableJoint();
        yield return new WaitForSeconds(0.25f);
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
