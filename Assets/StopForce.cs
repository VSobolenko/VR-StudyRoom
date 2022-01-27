using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopForce : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<ConstantForce>(out var go))
        {
            go.enabled = false;
        }
    }
}
