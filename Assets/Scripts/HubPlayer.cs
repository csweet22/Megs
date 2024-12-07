using System;
using TMPro;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Serialization;

public class HubPlayer : NetworkBehaviour
{
    private Rigidbody rb;

    private NetworkVariable<FixedString128Bytes> _displayName =
        new NetworkVariable<FixedString128Bytes>(writePerm: NetworkVariableWritePermission.Owner);

    [SerializeField] private TextMeshPro playerNameText;

    public override void OnNetworkSpawn()
    {
        UpdateDisplayName(_displayName.Value, _displayName.Value);
        _displayName.OnValueChanged += UpdateDisplayName;

        if (!IsOwner) return;

        base.OnNetworkSpawn();

        if (TryGetComponent(out Rigidbody _rb))
            rb = _rb;
        else
            Debug.LogError("Could not find rigidbody.");
        _displayName.Value = DBManager.username;
    }

    private void Update()
    {
        playerNameText.transform.forward = Camera.main.transform.forward;
    }

    private void UpdateDisplayName(FixedString128Bytes oldValue, FixedString128Bytes newValue)
    {
        playerNameText.text = newValue.ToString();
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

    public override void OnNetworkDespawn()
    {
        _displayName.OnValueChanged = null;
        base.OnNetworkDespawn();
    }
}