using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
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


        using (var w = UnityWebRequest.Post("http://localhost/sqlconnect/register.php/", form)){
            yield return w.SendWebRequest();
            Debug.Log("register.php request: " + w.result);
            if (w.result == UnityWebRequest.Result.Success){
                if (w.downloadHandler.text == "0"){
                    Debug.Log("User created successfully.");
                    SceneManager.LoadScene("MainMenu");
                }
                else{
                    Debug.Log("User creation failed. Error #" + w.downloadHandler.text);
                }
            }
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length > 3 && passwordField.text.Length > 7);
    }
}