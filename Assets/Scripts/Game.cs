using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private TMP_Text playerDisplay;
    [SerializeField] private TMP_Text scoreDisplay;

    private void Awake()
    {
        // Why not just check LoggedIn?
        if (DBManager.username == null){
            SceneManager.LoadScene("MainMenu");
        }

        playerDisplay.text = "Player: " + DBManager.username;
        scoreDisplay.text = "Score: " + DBManager.score;
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    private IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("score", DBManager.score);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;
        if (www.text == "0"){
            Debug.Log("Game saved.");
        }
        else{
            Debug.Log("Save failed. Error #" + www.text);
        }
        
        DBManager.LogOut();
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseScore()
    {
        DBManager.score++;
        scoreDisplay.text = "Score: " + DBManager.score;
    }
}