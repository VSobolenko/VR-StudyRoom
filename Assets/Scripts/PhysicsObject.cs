using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private Collider colliderBody;

    public Rigidbody Body => body;
    public Collider ColliderBody => colliderBody;

    private bool _isStartSimulation;

    private void Update()
    {
        if (_isStartSimulation)
        {
            body.WakeUp();
        }
    }

    public void StartSimulation()
    {
        if (ReferenceEquals(body, null))
        {
            return;
        }
        
        body.isKinematic = false;
        _isStartSimulation = true;
    }
    
    public void StopSimulation()
    {
        if (ReferenceEquals(body, null))
        {
            return;
        }
        
        body.isKinematic = true;
        _isStartSimulation = false;
    }
    
    private void OnValidate()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody>();
            if (body == null)
            {
                body = GetComponentInChildren<Rigidbody>();
            }
        }
        
        if (colliderBody == null)
        {
            colliderBody = GetComponent<Collider>();
            if (colliderBody == null)
            {
                colliderBody = GetComponentInChildren<Collider>();
            }
        }
    }
}
