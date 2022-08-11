using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDrag : MonoBehaviour
{
    [SerializeField] private float drag = 1f;
    private Rigidbody rb;
    [SerializeField] private Vector3 vel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        vel = rb.velocity;
        rb.AddForce(new Vector3(-vel.x * rb.mass * drag, 0, -vel.z* rb.mass * drag));
    }
}
