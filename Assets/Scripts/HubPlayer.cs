using System;
using Unity.Mathematics;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class HubPlayer : NetworkBehaviour
{
    private Rigidbody rb;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (TryGetComponent(out Rigidbody _rb))
            rb = _rb;
        else
            Debug.LogError("Could not find rigidbody.");
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;

        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
            moveDir += Vector3.right;
        if (Input.GetKey(KeyCode.A))
            moveDir -= Vector3.right;
        if (Input.GetKey(KeyCode.W))
            moveDir += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            moveDir -= Vector3.forward;

        float moveSpeed = 3.0f;

        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = moveDir.magnitude * moveDir.normalized * moveSpeed;
        Vector3 velocityDelta = (targetVelocity - currentVelocity);
        rb.AddForce(velocityDelta, ForceMode.VelocityChange);
    }
}