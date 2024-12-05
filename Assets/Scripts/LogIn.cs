using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField passwordField;

    [SerializeField] private Button submitButton;

    public void CallLogIn()
    {
        StartCoroutine(LogInPlayer());
    }

    private IEnumerator LogInPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);
        using (var w = UnityWebRequest.Post("http://localhost/sqlconnect/login.php/", form)){
            yield return w.SendWebRequest();
            Debug.Log("login.php request: " + w.result);
            if (w.result == UnityWebRequest.Result.Success){
                if (w.downloadHandler.text[0] == '0'){
                    DBManager.username = nameField.text;
                    DBManager.score = int.Parse(w.downloadHandler.text.Split("\t")[1]);
                    SceneManager.LoadScene("MainMenu");
                }
                else{
                    Debug.Log("User login failed. Error #" + w.downloadHandler.text);
                }
            }
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length > 3 && passwordField.text.Length > 7);
    }
}