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
    [SerializeField] private Button quitButton;

    void Start()
    {
        // Initialize which buttons are visible based on login state
        // Set display name.
        InitializeMenu();

        // Binding button behaviours.
        registerButton.onClick.AddListener(() => { SceneManager.LoadScene("RegisterMenu"); });
        loginButton.onClick.AddListener(() => { SceneManager.LoadScene("LogInMenu"); });
        playButton.onClick.AddListener(() => { SceneManager.LoadScene("Game"); });
        logoutButton.onClick.AddListener(LogOut);
        quitButton.onClick.AddListener(OnQuit);
    }

    private void InitializeMenu()
    {
        playerDisplay.text = DBManager.LoggedIn ? DBManager.username : "Not Logged In.";

        // If not logged in, show login & register buttons.
        registerButton.gameObject.SetActive(!DBManager.LoggedIn);
        loginButton.gameObject.SetActive(!DBManager.LoggedIn);

        // If logged in, show logout & play buttons.
        logoutButton.gameObject.SetActive(DBManager.LoggedIn);
        playButton.gameObject.SetActive(DBManager.LoggedIn);

        if (DBManager.LoggedIn){
            playButton.Select();
        }
        else{
            loginButton.Select();
        }
    }

    public void LogOut()
    {
        DBManager.LogOut();
        InitializeMenu();
    }

    private void OnQuit()
    {
        LogOut();
        Application.Quit();
    }

    private void OnDestroy()
    {
        registerButton.onClick.RemoveAllListeners();
        loginButton.onClick.RemoveAllListeners();
        playButton.onClick.RemoveAllListeners();
        logoutButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }
}