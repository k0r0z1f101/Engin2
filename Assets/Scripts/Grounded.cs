using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour, ICharacterState
{
    private Collider col;
    protected Rigidbody rb;
    private Vector3 colCenterBottomPoint;
    private float minDistToGround = 0.1f;
    [SerializeField] private LayerMask canJumpFrom;

    private void Awake()
    {
        canJumpFrom = ~(1 << 3);
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    public bool StateHandle(PlayerController pc)
    {
        return CheckIfGrounded();
    }

    private bool CheckIfGrounded()
    {
        colCenterBottomPoint = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        return Physics.CheckSphere(colCenterBottomPoint, minDistToGround, canJumpFrom);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(colCenterBottomPoint, minDistToGround);
    }
}
