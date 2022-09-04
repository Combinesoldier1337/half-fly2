using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftController : MonoBehaviour
{    
    [SerializeField] private float totalEnginePower, breakForce;
    [SerializeField] private float jetEnginePower;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float upforce;
    [SerializeField] private float currentSteeringAngle;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private WheelCollider[] m_WheelColliders;
    [SerializeField] private MeshFilter[] m_Wheels;
    [SerializeField] Vector3 _com;
    private AudioSource jetSnd;
    Rigidbody rb;
    private float currentJetPower, currentbreakForce;
    private bool isBreaking = false;
    SkinnedMeshRenderer _smr;

    float inputAxis_Vertical = 0;
    float inputAxis_Horizontal = 0;
    float inputAxis_Roll = 0;

    public bool SetBreaking { set { isBreaking = value; } }
    public float SetVerticalInput { set { inputAxis_Vertical = value; } }
    public float SetHorizontalInput { set { inputAxis_Horizontal = value; } }
    public float SetRollInput { set { inputAxis_Roll = value; } }


    public float getCurrentJetPower { get { return currentJetPower; } }
    public float getTotalJetPower { get { return jetEnginePower; } }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = _com;
        _smr = GetComponent<SkinnedMeshRenderer>();
        jetSnd = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        Glide();
        KeepUpright();
        HandleWings();
        UseJets();
    }

    private IEnumerator MoveChassis(float n)
    {
        yield return null;
    }

    private void Glide()
    {
        rb.AddRelativeForce(-Vector3.forward * rb.velocity.y, ForceMode.Acceleration);
    }


    private void KeepUpright()
    {
        if(rb.angularVelocity.magnitude > 1)
        {
            rb.AddRelativeTorque(-rb.angularVelocity, ForceMode.Impulse);            
        }
        jetSnd.pitch = ((rb.velocity.magnitude + currentJetPower) * 0.02f) + .4f;
    }


    public void HandleWings()
    {
        if (!Physics.Raycast(transform.position, -transform.up, 1) || rb.velocity.magnitude > 12)
        {
            rb.AddRelativeTorque(Vector3.right * (inputAxis_Vertical * 120), ForceMode.Impulse);
            rb.AddRelativeTorque(Vector3.forward * (-inputAxis_Horizontal * 120), ForceMode.Impulse);
            rb.AddRelativeTorque(Vector3.up * (inputAxis_Roll * 120), ForceMode.Impulse);

            //ShapeKeys
            _smr.SetBlendShapeWeight(1, (-inputAxis_Horizontal - inputAxis_Vertical) * 200);
            _smr.SetBlendShapeWeight(2, (inputAxis_Horizontal - inputAxis_Vertical) * 200);
            _smr.SetBlendShapeWeight(3, -inputAxis_Horizontal * 200);
            _smr.SetBlendShapeWeight(4, inputAxis_Horizontal * 200);
            _smr.SetBlendShapeWeight(5, -inputAxis_Roll * 200);
        }
    }

    public void Accelerate()
    {
        currentJetPower = currentJetPower > jetEnginePower ? jetEnginePower : currentJetPower + 0.1f;
    }

    public void Decelerate()
    {
        currentJetPower = currentJetPower <= 0 ? 1 : currentJetPower-0.2f;
    }
    private void UseJets()
    {
        rb.AddRelativeForce(Vector3.forward * currentJetPower, ForceMode.Acceleration);
    }

    private void HandleMotor()
    {
        for (int i = 0; i < m_WheelColliders.Length; i++)
        {
            if (inputAxis_Vertical != 0)
            {
                m_WheelColliders[i].motorTorque = Mathf.Lerp(m_WheelColliders[i].motorTorque, Mathf.Abs(inputAxis_Vertical) * totalEnginePower, turnSpeed);
            }
            else
            {
                m_WheelColliders[i].motorTorque = 0;
            }
        }
        AddDownForce();
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        for (int i = 0; i < m_WheelColliders.Length; i++)
        {
            m_WheelColliders[i].brakeTorque = currentbreakForce;
        }
    }

    private void AddDownForce()
    {
        rb.AddForce(transform.up * upforce * rb.velocity.magnitude);
    }

    private void HandleSteering()
    {
        currentSteeringAngle = (maxSteeringAngle * inputAxis_Horizontal);
        m_WheelColliders[0].steerAngle = Mathf.Lerp(m_WheelColliders[0].steerAngle, currentSteeringAngle, turnSpeed);
    }

    private void UpdateWheels()
    {
        for (int i = 0; i < m_WheelColliders.Length; i++)
        {
            UpdateSingleWheel(m_WheelColliders[i], m_Wheels[i].transform);
        }
    }
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
