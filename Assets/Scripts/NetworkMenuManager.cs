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
        if (!ValidateFields()) return;

        SetUpConnectionData();

        // Star the hosting.
        NetworkManager.Singleton.StartHost();

        NetworkManager.Singleton.SceneManager.LoadScene("Hub", LoadSceneMode.Single);
    }

    private void StartClient()
    {
        if (!ValidateFields()) return;

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

    private bool ValidateFields()
    {
        bool valid = true;

        if (addressField.text == "") valid = false;
        if (portField.text == "") valid = false;

        if (!valid)
            status.text = "Please enter a valid IP address & port.";

        return valid;
    }

    private void OnDestroy()
    {
        hostButton.onClick.RemoveAllListeners();
        joinButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
}