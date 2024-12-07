using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkChatManager : NetworkPersistentSingleton<NetworkChatManager>
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Debug.Log("Chat Spawned.");
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        Debug.Log("Chat Despawned.");
    }


    [Rpc(SendTo.Everyone)]
    private void LogClientRpc(string message, ulong clientId)
    {
        Debug.Log($"{clientId}: {message}");
    }

    [Rpc(SendTo.Server, RequireOwnership = false)]
    private void ServerLogServerRpc(string message, ulong clientId)
    {
        LogClientRpc(message, clientId);
    }

    public void Log(string message)
    {
        ServerLogServerRpc(message, NetworkManager.Singleton.LocalClientId);
    }
}