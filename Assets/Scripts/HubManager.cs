using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubManager : NetworkBehaviour
{
    [SerializeField] private Transform playerPrefab;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        NetworkManager.Singleton.OnClientConnectedCallback += SpawnHubPlayer;

        base.OnNetworkSpawn();

        List<ulong> clientIds = NetworkManager.Singleton.ConnectedClients.Keys.ToList();

        foreach (ulong clientId in clientIds){
            SpawnHubPlayer(clientId);
        }
    }

    private void SpawnHubPlayer(ulong clientId)
    {
        Transform instance = Instantiate(playerPrefab);

        // Ensure the prefab has a NetworkObject component
        NetworkObject networkObject = instance.GetComponent<NetworkObject>();
        if (networkObject == null){
            Debug.LogError("The playerPrefab does not have a NetworkObject component!");
            return;
        }

        // Spawn the object with ownership
        networkObject.SpawnWithOwnership(clientId, true);
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer) return;

        NetworkManager.Singleton.OnClientConnectedCallback -= SpawnHubPlayer;
        base.OnNetworkDespawn();
    }
}