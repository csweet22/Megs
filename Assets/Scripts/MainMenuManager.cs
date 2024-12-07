using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerDisplay;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button logoutButton;

    void Start()
    {
        InitializeMenu();

        registerButton.onClick.AddListener(() => { SceneManager.LoadScene("RegisterMenu"); });
        loginButton.onClick.AddListener(() => { SceneManager.LoadScene("LogInMenu"); });
        playButton.onClick.AddListener(() => { SceneManager.LoadScene("Game"); });
        logoutButton.onClick.AddListener(LogOut);
    }

    private void OnDestroy() => registerButton.onClick.RemoveAllListeners();

    private void InitializeMenu()
    {
        playerDisplay.text = DBManager.LoggedIn ? DBManager.username : "Not Logged In.";

        registerButton.gameObject.SetActive(!DBManager.LoggedIn);
        loginButton.gameObject.SetActive(!DBManager.LoggedIn);

        logoutButton.gameObject.SetActive(DBManager.LoggedIn);
        playButton.gameObject.SetActive(DBManager.LoggedIn);
    }

    public void LogOut()
    {
        DBManager.LogOut();
        InitializeMenu();
    }
}