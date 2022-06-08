using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveButton : MonoBehaviour
{
    public  Info _info;
    public  PhysicsObject _physicsObject;

    private void Start()
    {
        _physicsObject.StopSimulation();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<OVRGrabber>(out var test))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<CharacterController>(out var test2))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<OVRHand>(out var test3))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<FirstPersonMovement>(out var test4))
        {
            StartSimulation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<OVRGrabber>(out var test))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<CharacterController>(out var test2))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<OVRHand>(out var test3))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<FirstPersonMovement>(out var test4))
        {
            StartSimulation();
        }
    }

    public void StartSimulation()
    {
        //_info.StartSimulate();
        _physicsObject.StartSimulation();
    }
}
