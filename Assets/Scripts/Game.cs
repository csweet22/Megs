using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
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

        using (var w = UnityWebRequest.Post("http://localhost/sqlconnect/savedata.php/", form)){
            yield return w.SendWebRequest();
            Debug.Log("savedata.php request: " + w.result);
            if (w.result == UnityWebRequest.Result.Success){
                if (w.downloadHandler.text == "0"){
                    Debug.Log("Game saved.");
                }
                else{
                    Debug.Log("Save failed. Error #" + w.downloadHandler.text);
                }
            }
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