using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);

        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
        yield return www;
        if (www.text[0] == '0'){
            DBManager.username = nameField.text;
            DBManager.score = int.Parse(www.text.Split("\t")[1]);
            SceneManager.LoadScene("MainMenu");
        }
        else{
            Debug.Log("User login failed. Error #" + www.text);
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length > 8 && passwordField.text.Length > 8);
    }
}