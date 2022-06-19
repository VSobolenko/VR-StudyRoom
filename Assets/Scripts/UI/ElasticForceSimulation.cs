using System.Linq;
using TMPro;
using UnityEngine;

public class ElasticForceSimulation : PhysicsInformation
{
    [SerializeField] private TextMeshProUGUI deltaL;
    [SerializeField] private TextMeshProUGUI elasticForce;
    [SerializeField] private TextMeshProUGUI elasticMaxForce;
    [SerializeField] private TextMeshProUGUI hardnessFactor;

    private float K = 200f;
    
    private Vector3 _startPos;
    private Vector3 _maxPos;

    protected override void StartSetup()
    {
        base.StartSetup();
        
        _startPos = Body.transform.position;
        _maxPos = Body.transform.position;
    }

    protected override void UpdateInfo()
    {
        base.UpdateInfo();

        if (Vector3.Distance(_startPos, Body.transform.position) > Vector3.Distance(_startPos, _maxPos))
        {
            _maxPos = Body.transform.position;
        }

        var delta = Vector3.Distance(_startPos, _maxPos);
        var force = Vector3.Distance(_startPos, Body.transform.position) * K;
        var maxForce = Vector3.Distance(_startPos, _maxPos) * K;
        
        elasticForce.text = $"F = {force:#.##} Н";
        elasticMaxForce.text = $"Fmax = {maxForce:#.##} Н";
        deltaL.text = $"L = {delta:#.##} м";
        hardnessFactor.text = $"k = {K:#.##}";
    }

    public override void StartSimulation()
    {
        base.StartSimulation();
        
        _startPos = Body.transform.position;
        _maxPos = Body.transform.position;
    }
    
    public void ChangeHardnessFactor(bool isUp)
    {
        if (ReferenceEquals(Body, null))
        {
            return;
        }

        const float harnessDifferent = 100f;
        
        var add = isUp ? harnessDifferent : -harnessDifferent;
        if (Body.mass + add <= 0)
        {
            return;
        }
        
        Body.mass += add;
    }

    protected override void ValidateSetup()
    {
        if (deltaL == null)
        {
            deltaL = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "DeltaL");
        }
        
        if (elasticForce == null)
        {
            elasticForce = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Force");
        }
        
        if (elasticMaxForce == null)
        {
            elasticMaxForce = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Max Force");
        }
        
        if (hardnessFactor == null)
        {
            hardnessFactor = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Hardness Factor");
        }
        
        ValidateButton(transform, "Hardness Factor", ChangeHardnessFactor);
        base.ValidateSetup();
    }
}
