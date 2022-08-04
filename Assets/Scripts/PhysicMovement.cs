using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[System.Serializable]
public class PhysicMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    protected ForceMode _forceMode;
    protected sbyte _groundSpeed = 5;
    protected sbyte _maxGroundSpeed = 5;

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected void Move(Vector3 dir)
    {
        if (_rigidbody.velocity.magnitude < _maxGroundSpeed)
        {
            _rigidbody.AddForce(dir * _groundSpeed, _forceMode);
        }
    }
}
