using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhysics : MonoBehaviour
{
    //Offset values for the left hand; right uses a reversed rotationZ and positionX
    [SerializeField] private Vector3 positionOffset = new Vector3(-0.66f, -0.03f, -0.12f);
    [SerializeField] private Vector3 rotationOffset = new Vector3(-7.6f, -12.54f, 110.92f);

    [SerializeField] private GameObject followObject; //the corresponding hand with the controller
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f; //if too high, ends up rubberbanding; need to increase corresponding angular drag

    private Transform followTarget;
    private Rigidbody body;



    private void Start()
    {

        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        //reset inspector values
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;
        body.maxAngularVelocity = 20f;
        //teleport hands
        body.position = followTarget.position;
        body.rotation = followTarget.rotation;

    }

    private void Update()
    {
        PhysicsMove();
    }
    private void PhysicsMove()
    {
        //Position

        var positionWithOffset = followTarget.TransformPoint(positionOffset);

        var distance = Vector3.Distance(positionWithOffset, transform.position);
        body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        //Rotation

        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);

        var q = rotationWithOffset * Quaternion.Inverse(body.rotation); //gets distance between rotation of target and hand
        q.ToAngleAxis(out float angle, out Vector3 axis); //converts rotation to an angle and axis of rotation
        body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed); //sets rigidbody angular velocity
    }

}
