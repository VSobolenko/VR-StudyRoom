using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StandalonePerson : MainPerson
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    private void OnClick()
    {
        var eventData = new PointerEventData(EventSystem.current);
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        eventData.position = new Vector2(x, y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        if (results.Count > 0)
        {
            foreach (var raycastResult in results)
            {
                if (raycastResult.gameObject.TryGetComponent<Button>(out var button))
                {
                    button.onClick?.Invoke();
                }

                if (raycastResult.gameObject.TryGetComponent<LiveButton>(out var liveButton))
                {
                    liveButton.StartSimulation();
                }
            }
        }

        var ray = activeCamera.ScreenPointToRay(new Vector3(x, y));
        if (Physics.Raycast(ray.origin, ray.direction, out var info, 1000f))
        {
            Debug.DrawLine(ray.origin, info.point, Color.red, 5f);
            if (info.collider.gameObject.TryGetComponent<LiveButton>(out var liveButton2))
            {
                Debug.Log("START");
                liveButton2.StartSimulation();
            }
        }
    }
}
