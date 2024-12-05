using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private TMP_Text playerDisplay;
    void Start()
    {
        if (DBManager.LoggedIn){
            playerDisplay.text = "Player: " + DBManager.username;
        }
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
