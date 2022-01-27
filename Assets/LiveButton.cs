using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveButton : MonoBehaviour
{
    public  Info _info;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<OVRGrabber>(out var test))
        {
            _info.StartSimulate();
        }
        else if (other.gameObject.TryGetComponent<CharacterController>(out var test2))
        {
            _info.StartSimulate();
        }
        else if (other.gameObject.TryGetComponent<OVRHand.Hand>(out var test3))
        {
            _info.StartSimulate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<OVRGrabber>(out var test))
        {
            _info.StartSimulate();
        }
        else if (other.gameObject.TryGetComponent<CharacterController>(out var test2))
        {
            _info.StartSimulate();
        }
        else if (other.gameObject.TryGetComponent<OVRHand.Hand>(out var test3))
        {
            _info.StartSimulate();
        }
    }
}
