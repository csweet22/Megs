using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RegistrationManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordFieldA;
    [SerializeField] private TMP_InputField passwordFieldB;

    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private TextMeshProUGUI statusText;

    private void Start()
    {
        // Validate username and password values.
        usernameField.onValueChanged.AddListener(VerifyInputs);
        passwordFieldA.onValueChanged.AddListener(VerifyInputs);
        passwordFieldB.onValueChanged.AddListener(VerifyInputs);

        // Connect button behaviours.
        submitButton.onClick.AddListener(() => { StartCoroutine(Register()); });
        backButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }

    private IEnumerator Register()
    {
        // Reset user facing status message.
        statusText.text = "";

        // Create form for registration.
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordFieldA.text);

        // Call registration endpoint.
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
                    statusText.text = "Error #" + w.downloadHandler.text;
                }
            }
        }
    }

    private void VerifyInputs(string newValue)
    {
        // Check username is at least 4 characters.
        bool nameLongEnough = usernameField.text.Length >= 4;
        // Check password is at least 8 characters.
        bool passwordLongEnough = passwordFieldA.text.Length >= 8;
        // Check password is at least 8 characters.
        bool passwordsMatch = passwordFieldA.text == passwordFieldB.text;
        
        // Toggle submit button on if fields are valid.
        submitButton.interactable = nameLongEnough && passwordLongEnough && passwordsMatch;

        // Display user facing status message.
        statusText.text = "";
        if (!nameLongEnough)
            statusText.text += "\nUsername must be at least 4 characters long.";
        if (!passwordLongEnough)
            statusText.text += "\nPassword must be at least 8 characters long.";
        if (!passwordsMatch)
            statusText.text += "\nPassword do not match.";
    }
}