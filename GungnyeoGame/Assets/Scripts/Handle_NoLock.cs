using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle_NoLock : MonoBehaviour
{
    // To be expanded on later so it can be re-latched

    private bool activated = false;

    public void SetRigidBodyGravity()
    {

        if (activated) return;

        GetComponent<Rigidbody>().useGravity = true;

        if (transform.childCount > 0)
        {
            Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rigid in rb)
            {
                rigid.useGravity = true;
            }
            
        }
    }



}
