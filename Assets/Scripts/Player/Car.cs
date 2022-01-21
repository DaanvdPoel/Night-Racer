using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.centerOfMass = m_settings.centerOfMass;
    }

    public void FixedUpdate()
    {
        Steering();
        DownForce();
        Drift();
    }

    private void Update()
    {
    }

    private void Steering()
    {
        float _motor = m_settings.maxMotorTorque * Input.GetAxis("Vertical");
        float _steering = m_settings.maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo _axleInfo in m_axleInfos)
        {
            if (_axleInfo.steering)
            {
                _axleInfo.leftWheel.steerAngle = _steering;
                _axleInfo.rightWheel.steerAngle = _steering;
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
                _axleInfo.leftWheel.brakeTorque = 0;
                _axleInfo.rightWheel.brakeTorque = 0;
            }
        }
        foreach (AxleInfo _axleInfo in m_axleInfos)
        {
            if (_axleInfo.motor)
            {
                _axleInfo.leftWheel.brakeTorque = (m_settings.maxMotorTorque * 1.1f) * Input.GetAxisRaw("Break");
                _axleInfo.rightWheel.brakeTorque = (m_settings.maxMotorTorque * 1.1f) * Input.GetAxisRaw("Break");
            }
        }
    }

    private void DownForce()
    {
        m_rigidBody.AddForce(-Vector3.up * m_settings.downForce, ForceMode.Force);
    }
}
