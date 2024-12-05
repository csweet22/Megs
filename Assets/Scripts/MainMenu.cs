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

    void Start()
    {
        if (DBManager.LoggedIn){
            playerDisplay.text = "Player: " + DBManager.username;
        }

        registerButton.gameObject.SetActive(!DBManager.LoggedIn);
        loginButton.gameObject.SetActive(!DBManager.LoggedIn);
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

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
}