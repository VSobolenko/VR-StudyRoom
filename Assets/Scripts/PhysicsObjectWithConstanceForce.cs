using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjectWithConstanceForce : PhysicsObject
{
    [SerializeField] private ConstantForce constantForceBody;
    protected override void ValidateSetup()
    {
        base.ValidateSetup();
        if (constantForceBody == null)
        {
            constantForceBody = GetComponent<ConstantForce>();
            if (constantForceBody == null)
            {
                constantForceBody = GetComponentInChildren<ConstantForce>();
            }
        }
    }
}
