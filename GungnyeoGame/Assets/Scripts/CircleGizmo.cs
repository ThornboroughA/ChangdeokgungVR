using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CircleGizmo : MonoBehaviour
{
    public float radius = 1f;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, new Vector3(0, 1, 0), radius);
    }
#endif
}