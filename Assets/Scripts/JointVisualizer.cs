using System;
using UnityEngine;

[ExecuteInEditMode]
public class JointVisualizer : MonoBehaviour
{
    [SerializeField] private Joint hingeJointBody;
    [SerializeField] private LineRenderer lineRenderer;

    private void Update()
    {
        if (hingeJointBody == null || lineRenderer == null)
        {
            return;
        }
        
        lineRenderer.SetPosition(0,  hingeJointBody.transform.position);
        lineRenderer.SetPosition(1,  hingeJointBody.connectedAnchor);
    }

    private void OnValidate()
    {
        if (hingeJointBody == null)
        {
            hingeJointBody = GetComponent<Joint>();
            if (hingeJointBody == null)
            {
                hingeJointBody = GetComponentInChildren<Joint>();
            }
        }
        
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                lineRenderer = GetComponentInChildren<LineRenderer>();
            }
        }
    }
}
