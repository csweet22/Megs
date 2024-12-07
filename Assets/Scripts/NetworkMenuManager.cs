using System;
using System.Collections;
using System.Net;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NetworkMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField addressField;
    [SerializeField] private TMP_InputField portField;

    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private TMP_Text status;

    private void Awake()
    {
        if (!DBManager.LoggedIn){
            SceneManager.LoadScene("MainMenu");
        }

        hostButton.onClick.AddListener(StartHost);
        joinButton.onClick.AddListener(StartClient);

        exitButton.onClick.AddListener(() => { SceneManager.LoadScene("MainMenu"); });

        NetworkManager.Singleton.OnClientConnectedCallback += id => { Debug.Log($"Client {id} connected."); };
        NetworkManager.Singleton.OnClientDisconnectCallback += id => { Debug.Log($"Client {id} disconnected."); };
    }

    private void StartHost()
    {
        ValidateFields();

        SetUpConnectionData();

        // Star the hosting.
        NetworkManager.Singleton.StartHost();

        NetworkManager.Singleton.SceneManager.LoadScene("Hub", LoadSceneMode.Single);
    }

    private void StartClient()
    {
        ValidateFields();

        SetUpConnectionData();

        NetworkManager.Singleton.StartClient();

        NetworkManager.Singleton.SceneManager.LoadScene("Hub", LoadSceneMode.Single);
    }

    private void SetUpConnectionData()
    {
        // Set host address & port.
        UnityTransport transport = NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();
        transport.ConnectionData.Address = addressField.text;
        transport.ConnectionData.Port = Convert.ToUInt16(portField.text);
    }

    private void ValidateFields()
    {
        if (addressField.text == "") addressField.text = "127.0.0.1";
        if (portField.text == "") portField.text = "7777";
    }

    private void OnDestroy()
    {
        hostButton.onClick.RemoveAllListeners();
        joinButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
}