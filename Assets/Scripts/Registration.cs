using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Registration : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField passwordField;

    [SerializeField] private Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    private IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);
        
        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;
        if (www.text == "0"){
            Debug.Log("User created successfully.");
            SceneManager.LoadScene("MainMenu");
        }
        else{
            Debug.Log("User creation failed. Error #" + www.text);
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length > 3 && passwordField.text.Length > 7);
    }
}