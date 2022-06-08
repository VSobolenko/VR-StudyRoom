using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public Button buttonStart;
    public GameObject panelObj;
    public GameObject canvas;
    public TextMeshProUGUI massText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI velocityText;
    
    public bool IsSopr;
    public bool IsBall;
    public bool IsMayatnik;
    public bool IsInert;

    private ConstantForce _constantForce;
    private Rigidbody _rigidbody;
    
    private void Start()
    {
        //buttonStart.onClick.AddListener(StartSimulate);
        _constantForce = GetComponent<ConstantForce>();
        _rigidbody = GetComponent<Rigidbody>();
        if (_constantForce != null)
        {
            _constantForce.enabled = false;
        }
        
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true;
        }
        panelObj.SetActive(false);
    }

    public void StartSimulate()
    {
        canvas.gameObject.transform.parent = null;
        buttonStart.gameObject.SetActive(false);
        panelObj.SetActive(true);
        
        //canvas.transform.LookAt(Camera.main.transform);
        if (_constantForce != null)
        {
            _constantForce.enabled = true;
        }
        
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = false;
        }
    }

    public void StopSimulation()
    {
        
    }
    
    private void Update()
    {
        if (!ReferenceEquals(_rigidbody, null))
        {
            massText.text = "Mass = " + _rigidbody.mass;
            speedText.text = "Speed = " + _rigidbody.velocity.magnitude;
            velocityText.text = "Velocity = " + _rigidbody.velocity;
        }
    }
}
