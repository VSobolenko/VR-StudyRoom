using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Button _button;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _button.onClick.Invoke();
    }

    private void OnValidate()
    {
        if (_button == null)
        {
            _button = GetComponent<Button>();
        }
    }
}
