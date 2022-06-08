using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class PhysicsInformation : MonoBehaviour
{
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
        SetupInfo();
    }

    private void Update()
    {
        UpdateInfo();
    }

    protected virtual void SetupInfo()
    {
    }
    
    protected virtual void UpdateInfo()
    {
        if (ReferenceEquals(Body, null))
        {
            return;
        }
        
        massInfo.text = $"m = {Body.mass:#.##} кг";
        
        var velocity = Body.velocity;
        
        speedInfo.text = $"v = {velocity.magnitude:#.##} м/с";
        velocityInfo.text = $"v` = {velocity.ToString()} м/с";

        if (_accessTimer)
        {
            _differentTime = _startTime - DateTime.Now;
        }
        timeInfo.text = $"t = {_differentTime:mm\\:ss} с";

        if (_accessDistance)
        {
            _distance += Vector3.Distance(_previousPosition, Body.position);
            _previousPosition = Body.position;
        }
        
        distanceInfo.text = $"s = {_distance:#.##} м";
    }

    public void ChangeMass(bool isUp)
    {
        if (ReferenceEquals(Body, null))
        {
            return;
        }

        const float massDifferent = 0.5f;
        
        var add = isUp ? massDifferent : -massDifferent;
        if (Body.mass <= Body.mass - add)
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
    
    private void OnValidate()
    {
        ValidateSetup();
    }

    protected virtual void ValidateSetup()
    {
        if (physicsObject == null)
        { 
            physicsObject = transform.parent.parent.GetComponentInChildren<PhysicsObject>();
        }
        massInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Mass");
        speedInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Speed");
        velocityInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Velocity");
        timeInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Time");
        distanceInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Distance");
    }
}
