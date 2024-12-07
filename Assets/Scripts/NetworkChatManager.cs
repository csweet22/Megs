using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkChatManager : NetworkSingleton<NetworkChatManager>
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
    private void LogClientRpc(string message, string playerUsername)
    {
        Debug.Log($"{playerUsername}: {message}");
    }

    [Rpc(SendTo.Server, RequireOwnership = false)]
    private void ServerLogServerRpc(string message, string playerUsername)
    {
        LogClientRpc(message, playerUsername);
    }

    public void Log(string message)
    {
        ServerLogServerRpc(message, DBManager.username);
    }
}