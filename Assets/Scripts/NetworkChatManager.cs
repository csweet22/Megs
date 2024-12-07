using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkChatManager : NetworkSingleton<NetworkChatManager>
{
    [SerializeField] private TMP_Text chatText;
    [SerializeField] private TMP_InputField chatInputField;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Debug.Log("Chat Spawned.");
        chatInputField.onSubmit.AddListener((string message) =>
        {
            Log(message);
            chatInputField.text = "";
        });
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        Debug.Log("Chat Despawned.");
    }


    [Rpc(SendTo.Everyone)]
    private void LogClientRpc(string message, string playerUsername)
    {
        string fullMessage = $"<color=yellow>{playerUsername}</color>: {message}\n";
        chatText.text += fullMessage;
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