using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageConfig : MonoBehaviour
{
    [SerializeField] private Transform[] lookAtPeron;

    private void Update()
    {
        if (lookAtPeron != null)
        {
            foreach (var o in lookAtPeron)
            {
                o.LookAt(PlatformDependency.Singleton.ActivePerson.transform);
            }
        }
    }
}
