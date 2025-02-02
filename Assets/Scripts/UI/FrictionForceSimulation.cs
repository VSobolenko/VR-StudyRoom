
using System.Linq;
using TMPro;
using UnityEngine;

public class FrictionForceSimulation : PhysicsInformation
{
    [Header("Friction")]   
    [SerializeField] private TextMeshProUGUI alphaAngleInfo;
    [SerializeField] private TextMeshProUGUI frictionCoefficientInfo;
    [SerializeField] private Transform alphaGameObject;
    [SerializeField] private TextMeshProUGUI frictionForceInfo;

    protected float Angle => alphaGameObject.rotation.eulerAngles.z - 90;
    protected float Friction  => ColliderBody.material.dynamicFriction;
    
    protected override void UpdateInfo()
    {
        base.UpdateInfo();
        
        if (ReferenceEquals(Body, null) || ReferenceEquals(ColliderBody, null))
        {
            return;
        }
        
        alphaAngleInfo.text = $"A = {Mathf.Abs(Angle)} *";
        frictionCoefficientInfo.text = $"M = {Friction:#.##}";
        
        var force = Friction * Mass * AccelerationOfGravity * Mathf.Cos(Angle);
        frictionForceInfo.text = $"F = {Mathf.Abs(force):#.##} Н";
    }
    public void ChangeAngle(bool isUp)
    {
        if (ReferenceEquals(Body, null))
        {
            return;
        }

        const float angleDifferent = 1f;
        
        var add = isUp ? angleDifferent : -angleDifferent;

        var angle = alphaGameObject.rotation.eulerAngles;
        alphaGameObject.rotation = Quaternion.Euler(new Vector3(angle.x, angle.y, angle.z + add));
    }
    
    public void ChangeFrictionCoefficient(bool isUp)
    {
        if (ReferenceEquals(Body, null))
        {
            return;
        }

        const float frictionCoefficientDifferent = 0.01f;
        var add = isUp ? frictionCoefficientDifferent : -frictionCoefficientDifferent;
        
        ColliderBody.material.dynamicFriction += add;
        ColliderBody.material.staticFriction += add;
    }

    protected override void ValidateSetup()
    {
        if (alphaAngleInfo == null)
        {
            alphaAngleInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Angle");
        }

        if (frictionForceInfo == null)
        {
            frictionForceInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Friction Force");
        }

        if (frictionCoefficientInfo == null)
        {
            frictionCoefficientInfo = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Friction Coefficient");
        }

        base.ValidateSetup();
    }
}
