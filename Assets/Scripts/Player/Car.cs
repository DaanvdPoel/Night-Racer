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
        Steering();
        Drift();

        m_currentSpeed = m_rigidBody.velocity.magnitude * 3.6f;
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// Applies steering and torgue to the car.
    /// </summary>
    private void Steering()
    {
        float _motor = m_settings.maxMotorTorque * Input.GetAxisRaw("Vertical");

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
        if (Input.GetAxis("Break") > 0.1f)
        {
            //foreach (AxleInfo _axleInfo in m_axleInfos)
            //{
            //    if (_axleInfo.motor)
            //    {
            //        _axleInfo.leftWheel.motorTorque = 0;
            //        _axleInfo.rightWheel.motorTorque = 0;

            //    }
            //}
            Vector3 _newVelocity = new Vector3(m_rigidBody.velocity.x, m_rigidBody.velocity.y, 0);
            m_rigidBody.velocity = _newVelocity;
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
