using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTireModel : MonoBehaviour
{
    private WheelCollider m_parentCollider;         // Reference to the wheelcollider of this gameobjects parent.

    private void Start()
    {
        // Retrieve the corresponding reference.
        m_parentCollider = gameObject.GetComponentInParent<WheelCollider>();
    }

    private void Update()
    {
        // Update it's local position to reflect the parents wheelcollider.
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, m_parentCollider.steerAngle - transform.localEulerAngles.z, transform.localEulerAngles.z);
        transform.Rotate(m_parentCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }
}
