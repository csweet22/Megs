using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text playerDisplay;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button logoutButton;

    void Start()
    {
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        if (DBManager.LoggedIn){
            playerDisplay.text = "Player: " + DBManager.username;
        }
        else{
            playerDisplay.text = "Not Logged In.";
        }

        registerButton.gameObject.SetActive(!DBManager.LoggedIn);
        loginButton.gameObject.SetActive(!DBManager.LoggedIn);
        
        logoutButton.gameObject.SetActive(DBManager.LoggedIn);
        playButton.gameObject.SetActive(DBManager.LoggedIn);
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene("RegisterMenu");
    }

    public void GoToLogIn()
    {
        SceneManager.LoadScene("LogInMenu");
    }

    public void LogOut()
    {
        DBManager.LogOut();
        InitializeMenu();
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
}