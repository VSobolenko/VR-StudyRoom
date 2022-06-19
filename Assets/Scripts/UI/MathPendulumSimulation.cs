using System.Linq;
using TMPro;
using UnityEngine;

public class MathPendulumSimulation : PhysicsInformation
{
    [SerializeField] private TextMeshProUGUI length;
    [SerializeField] private TextMeshProUGUI angle;

    private HingeJoint _hingeJointBody;
    private float _lengthHinge;
    private float _angleHinge;

    protected override void StartSetup()
    {
        base.StartSetup();

        _hingeJointBody = Body.gameObject.GetComponent<HingeJoint>();
    }

    protected override void UpdateInfo()
    {
        base.UpdateInfo();

        if (ReferenceEquals(_hingeJointBody, null))
        {
            return;
        }

        if (_accessLength)
        {
            _lengthHinge = Vector3.Distance(_hingeJointBody.transform.position, _hingeJointBody.anchor);
        }

        if (_accessAngle)
        {
            _angleHinge = Vector3.Angle(_hingeJointBody.anchor - Body.transform.position, _hingeJointBody.anchor);
        }

        length.text = $"l = {_lengthHinge:#.##} м";
        angle.text = $"a = {_angleHinge:#.##} *";
    }

    public override void StartSimulation()
    {
        StopLength();
        StopAngle();
        base.StartSimulation();
    }

    private bool _accessLength = true;

    public void StartLength()
    {
        _lengthHinge = Vector3.Distance(_hingeJointBody.transform.position, _hingeJointBody.anchor);
        _accessLength = true;
    }
    
    public void StopLength()
    {
        _lengthHinge = Vector3.Distance(_hingeJointBody.transform.position, _hingeJointBody.anchor);
        _accessLength = false;
    }
    
    private bool _accessAngle = true;

    public void StartAngle()
    {
        _lengthHinge = Vector3.Distance(_hingeJointBody.transform.position, _hingeJointBody.anchor);
        _accessAngle = true;
    }
    
    public void StopAngle()
    {
        _lengthHinge = Vector3.Distance(_hingeJointBody.transform.position, _hingeJointBody.anchor);
        _accessAngle = false;
    }
    
    public void ChangeLength(bool isUp)
    {
        var pos = _hingeJointBody.anchor;
        
        const float lengthDifferent = 0.5f;
        
        var add = isUp ? lengthDifferent : -lengthDifferent;
        if (pos.y + add <= 2)
        {
            add = -pos.y + 2;
        }
        
        _hingeJointBody.anchor = new Vector3(pos.x, pos.y + add, pos.z);
    }
    
    public void ChangeAngle(bool isUp)
    {
        var pos = _hingeJointBody.anchor;
        
        const float angleDifferent = 0.5f;
        
        var add = isUp ? angleDifferent : -angleDifferent;
        
        if (pos.z + add > 0)
        {
            add = pos.z;
        }
        _hingeJointBody.anchor = new Vector3(pos.x, pos.y, pos.z + add);
    }

    protected override void ValidateSetup()
    {
        if (length == null)
        {
            length = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Length");
        }
        
        if (angle == null)
        {
            angle = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Angle");
        }
        
        ValidateButton(transform, "Length", ChangeLength);
        ValidateButton(transform, "Angle", ChangeAngle);

        base.ValidateSetup();
    }
}
