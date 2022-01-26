using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarSettings", order = 0)]
public class CarSettings : ScriptableObject
{
    // Unityeditor accesible variables.
    [Header("General Settings:")]
    public float minReductedSteerAngle;             // The minimum amoutn currentspeed can reduce youre steering.
    public float maxSteeringAngle;                  // Maximum steer angle the wheel can have.
    public float maxSpeed;                          // The maximum expected speed the car will drive.
    public float maxMotorTorque;                    // Maximum torque the motor can apply to wheel
    public float downForce;                         // How much foirce to apply down on the car.
    public Vector3 centerOfMass;                    // Value to set the height of rigidbody CenterOfMass

    public AnimationCurve SteerAngleReduction;      // X axis is current speed between o and amxspeed, y is the angle between 0 and maxsteeringangle.
}
