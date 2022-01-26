using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;                          // Is this wheel attached to motor?
    public bool steering;                       // Does this wheel apply steer angle?
}

public class Car : MonoBehaviour
{
    // Unityeditor accesible variables.
    [Header("General Settings:")]
    [SerializeField] private List<AxleInfo> m_axleInfos;            // The information about each individual axle

    [Space]
    [SerializeField] private CarSettings m_settings;

    // Unityeditor unaccesible variables.
    private Rigidbody m_rigidBody;
    private float m_currentSpeed;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.centerOfMass = m_settings.centerOfMass;
    }

    public void FixedUpdate()
    {     
        DownForce();
        Drift();

        m_currentSpeed = m_rigidBody.velocity.magnitude * 3.6f;
    }

    private void Update()
    {
        Steering();
    }

    /// <summary>
    /// Applies steering and torgue to the car.
    /// </summary>
    private void Steering()
    {
        float _motor = m_settings.maxMotorTorque * Input.GetAxis("Vertical");

        foreach (AxleInfo _axleInfo in m_axleInfos)
        {
            if (_axleInfo.steering)
            {
                _axleInfo.leftWheel.steerAngle = LimitSteering();
                _axleInfo.rightWheel.steerAngle = LimitSteering();
            }
            if (_axleInfo.motor)
            {
                _axleInfo.leftWheel.motorTorque = _motor;
                _axleInfo.rightWheel.motorTorque = _motor;
            }            
        }
    }

    /// <summary>
    /// Switches the front and side friction of the back wheels.
    /// </summary>
    private void Drift()
    {
        foreach (AxleInfo _axleInfo in m_axleInfos)
        {
            if (_axleInfo.motor)
            {
                _axleInfo.leftWheel.brakeTorque = m_settings.maxMotorTorque * m_currentSpeed * Input.GetAxisRaw("Break");
                _axleInfo.rightWheel.brakeTorque = m_settings.maxMotorTorque * m_currentSpeed * Input.GetAxisRaw("Break");
            }
        }
    }

    /// <summary>
    /// Limits the maximum steering angle depending on the speed of the car.
    /// </summary>
    private float LimitSteering()
    {
        float _normalisedSteerReduction = m_settings.SteerAngleReduction.Evaluate((Mathf.Clamp(m_currentSpeed, 0, m_settings.maxSpeed) / m_settings.maxSpeed));
        float _resultingSteerReduction = m_settings.maxSteeringAngle * _normalisedSteerReduction;

        float _finalSteerAngle = Mathf.Clamp(_resultingSteerReduction, m_settings.minReductedSteerAngle, m_settings.maxSteeringAngle) * Input.GetAxis("Horizontal");
        return _finalSteerAngle;
    }

    /// <summary>
    /// Applies downforce to the car.
    /// </summary>
    private void DownForce()
    {
        m_rigidBody.AddForce(-transform.up * m_settings.downForce, ForceMode.Force);
    }
}
