using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PhysicsInformation : MonoBehaviour
{
    [Header("Settings")] [SerializeField] private Canvas _mainCanvas;
    
    [Header("General")]
    [SerializeField] protected PhysicsObject physicsObject;

    [SerializeField] private TextMeshProUGUI massInfo;
    [SerializeField] private TextMeshProUGUI speedInfo;
    [SerializeField] private TextMeshProUGUI velocityInfo;
    [SerializeField] private TextMeshProUGUI timeInfo;
    [SerializeField] private TextMeshProUGUI distanceInfo;

    protected float Mass => Body.mass;
    protected float AccelerationOfGravity  => Physics.gravity.y;
    protected Rigidbody Body => physicsObject.Body;
    protected Collider ColliderBody => physicsObject.ColliderBody;
    
    private TimeSpan _differentTime;

    private void Start()
    {
        StartSetup();
    }

    protected virtual void StartSetup()
    {
    }

    private void Update()
    {
        UpdateInfo();
    }
    
    protected virtual void UpdateInfo()
    {
        if (ReferenceEquals(Body, null))
        {
            return;
        }
        
        if (_accessTimer)
        {
            _differentTime = _startTime - DateTime.Now;
        }
        if (_accessDistance)
        {
            _distance += Vector3.Distance(_previousPosition, Body.position);
            _previousPosition = Body.position;
        }
        var velocity = Body.velocity;

        massInfo.text = $"m = {Body.mass:#.##} кг";
        speedInfo.text = $"v = {velocity.magnitude:#.##} м/с";
        velocityInfo.text = $"v` = {velocity.ToString()} м/с";
        timeInfo.text = $"t = {_differentTime:mm\\:ss} с";
        distanceInfo.text = $"s = {_distance:#.##} м";
    }

    public virtual void StartSimulation()
    {
        StartTimer();
        StartDistance();
    }

    public void SetupEventCamera(Camera eventCamera)
    {
        if (_mainCanvas == null)
        {
            try
            {
                _mainCanvas = transform.parent.GetComponentInChildren<Canvas>();
            }
            catch (Exception e)
            {
                Debug.Log($"Error validate in {gameObject.name}; Exception: {e.Message}");
            }
        }
        
        _mainCanvas.worldCamera = eventCamera;
    }
    
    #region Mass & Timer & Distance

    public void ChangeMass(bool isUp)
    {
        if (ReferenceEquals(Body, null))
        {
            return;
        }

        const float massDifferent = 0.5f;
        
        var add = isUp ? massDifferent : -massDifferent;
        if (0.1f > Body.mass + add)
        {
            return;
        }
        
        Body.mass += add;
    }

    private bool _accessTimer;
    private DateTime _startTime;
    
    public void StartTimer()
    {
        _accessTimer = true;
        _startTime = DateTime.Now;
    }

    public void StopTimer()
    {
        _accessTimer = false;
    }

    public void ResetTimer()
    {
        StopTimer();
        _differentTime = TimeSpan.Zero;
    }
    
    private bool _accessDistance;
    private float _distance;
    private Vector3 _previousPosition;
    
    public void StartDistance()
    {
        _accessDistance = true;
        _previousPosition = Body.position;
    }

    public void StopDistance()
    {
        _accessDistance = false;
    }

    public void ResetDistance()
    {
        _distance = 0;
    }

    #endregion

    #region Validate

    private void OnValidate()
    {
        ValidateSetup();
    }

    protected virtual void ValidateSetup()
    {
        if (_mainCanvas == null)
        {
            try
            {
                _mainCanvas = transform.parent.GetComponentInChildren<Canvas>();
            }
            catch (Exception e)
            {
                Debug.Log($"Error validate in {gameObject.name}; Exception: {e.Message}");
            }
        }
        if (physicsObject == null)
        {
            try
            {
                physicsObject = transform.parent.parent.GetComponentInChildren<PhysicsObject>();
            }
            catch (Exception e)
            {
                Debug.Log($"Error validate in {gameObject.name}; Exception: {e.Message}");
            }
        }

        if (massInfo == null)
        {
            massInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Mass");
        }

        if (speedInfo == null)
        {
            speedInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Speed");
        }

        if (velocityInfo == null)
        {
            velocityInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Velocity");
        }

        if (timeInfo == null)
        {
            timeInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Time");
        }

        if (distanceInfo == null)
        {
            distanceInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Distance");
        }

        ValidateButton(transform, "Mass", ChangeMass);
    }

    protected void ValidateButton(Transform startPoint, string rootName, UnityAction<bool> clickAction, int index = 0)
    {
        var angleRoot = startPoint.Find(rootName);
        if (angleRoot != null)
        {
            var addRootButton = angleRoot.Find("Plus");
            var removeRootButton = angleRoot.Find("Minus");
            
            if (addRootButton != null)
            {
                var addButton = addRootButton.GetComponentInChildren<Button>();
                if (addButton != null)
                {
#if UNITY_EDITOR
                    UnityEditor.Events.UnityEventTools.RegisterBoolPersistentListener(addButton.onClick, index, clickAction, true);
#endif
                }
            }

            if (removeRootButton != null)
            {
                var removeButton = removeRootButton.GetComponentInChildren<Button>();
                if (removeButton != null)
                {
#if UNITY_EDITOR
                    UnityEditor.Events.UnityEventTools.RegisterBoolPersistentListener(removeButton.onClick, index, clickAction, false);
#endif
                }
            }
        }
    }

    [ContextMenu("Reset Validate")]
    private void ResetValidate()
    {
        OnValidate();
    }
    #endregion
}
