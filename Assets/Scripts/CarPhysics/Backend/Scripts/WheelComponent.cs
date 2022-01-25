using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelPhysics
{
    public class WheelComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody m_carBodyRigidbody;          // The rigidbody attached to the cars main chassis.
        [SerializeField] private float m_springStartHeight;             // The height (Usually middle of wheel) were the spring is attached, measured from wheel base.
    }
}
