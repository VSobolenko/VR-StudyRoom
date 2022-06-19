using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPerson : MonoBehaviour
{
    public Camera activeCamera;
    public GameObject[] visibleObjects;
    
    public virtual void SetActiveObject(bool status)
    {
        if (visibleObjects == null)
        {
            return;
        }
        foreach (var notVisibleObject in visibleObjects)
        {
            if (notVisibleObject != null)
            {
                notVisibleObject.gameObject.SetActive(status);
            }
        }
    }
}
