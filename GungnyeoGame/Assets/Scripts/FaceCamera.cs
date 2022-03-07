using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] private Transform faceObject;

    private void Update()
    {
        Vector3 loc = new Vector3(faceObject.position.x, transform.position.y, faceObject.position.z);

        transform.LookAt(transform.position - (loc - transform.position));
    }


}
