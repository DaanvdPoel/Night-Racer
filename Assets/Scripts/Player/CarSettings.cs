using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarSettings", order = 0)]
public class CarSettings : ScriptableObject
{
    // Unityeditor accesible variables.
    [Header("General Settings:")]
    public float maxMotorTorque;            // Maximum torque the motor can apply to wheel
    public float maxSteeringAngle;          // Maximum steer angle the wheel can have
    public float downForce;                 // How much foirce to apply down on the car.
    public Vector3 centerOfMass;            // Value to set the height of rigidbody CenterOfMass
}
