using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPerson : MonoBehaviour
{
    public GameObject[] visibleObjects;
    
    public virtual void SetActiveObject(bool status)
    {
        foreach (var notVisibleObject in visibleObjects)
        {
            notVisibleObject.gameObject.SetActive(status);
        }
    }
}
