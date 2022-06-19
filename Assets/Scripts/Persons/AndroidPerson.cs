using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AndroidPerson : MainPerson
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick(Input.mousePosition);
        }
    }

    private void OnClick(Vector3 mousePosition)
    {
        var ray = activeCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out var info, 1000f))
        {
            Debug.Log($"Success Ray = {info.collider.gameObject.name}");
            Debug.DrawLine(ray.origin, info.point, Color.red, 5f);
            if (info.collider.gameObject.TryGetComponent<LiveButton>(out var liveButton2))
            {
                liveButton2.StartSimulation();
            }
        }
    }
    
    public void SetPersonPosition(Transform personPoint)
    {
        transform.position = new Vector3(personPoint.position.x, personPoint.position.y, personPoint.position.z);
        transform.rotation = new Quaternion(personPoint.rotation.x, personPoint.rotation.y, personPoint.rotation.z, personPoint.rotation.w);
    }
}
