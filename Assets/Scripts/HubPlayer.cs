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

        if (TryGetComponent<Rigidbody>(out Rigidbody _rb))
            rb = _rb;
        else
            Debug.LogError("Could not find rigidbody.");
    }

    private void Update()
    {
        if (!IsOwner) return;

        Vector3 moveDir = new Vector3(Mathf.Round(Input.GetAxis("Horizontal")), 0,
            Mathf.Round(Input.GetAxis("Vertical")));

        float moveSpeed = 3.0f;

        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = moveDir.magnitude * moveDir.normalized * moveSpeed;
        Vector3 velocityDelta = (targetVelocity - currentVelocity) * Time.deltaTime;
        rb.AddForce(velocityDelta, ForceMode.VelocityChange);
    }
}