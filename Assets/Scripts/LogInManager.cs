using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogInManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;

    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private TextMeshProUGUI statusText;

    private void Start()
    {
        // Toggle button if there are field values.
        usernameField.onValueChanged.AddListener(VerifyInputs);
        passwordField.onValueChanged.AddListener(VerifyInputs);

        // Connect button behaviours.
        submitButton.onClick.AddListener(() => { StartCoroutine(LogInPlayer()); });
        backButton.onClick.AddListener(() => { SceneManager.LoadScene("MainMenu"); });
    }

    private IEnumerator LogInPlayer()
    {
        // Clear user facing status text.
        statusText.text = "";

        // Create submission form.
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        
        // Attempt login to server.
        using (var w = UnityWebRequest.Post("http://localhost/sqlconnect/login.php/", form)){
            yield return w.SendWebRequest();
            Debug.Log("login.php request: " + w.result);
            if (w.result == UnityWebRequest.Result.Success){
                if (w.downloadHandler.text[0] == '0'){
                    DBManager.username = usernameField.text;
                    DBManager.score = int.Parse(w.downloadHandler.text.Split("\t")[1]);
                    SceneManager.LoadScene("MainMenu");
                }
                else{
                    Debug.Log("User login failed. Error #" + w.downloadHandler.text);
                    statusText.text += "\n" + w.downloadHandler.text;
                }
            }
        }
    }

    private void VerifyInputs(string newValue) =>
        submitButton.interactable = (usernameField.text.Length > 0 && passwordField.text.Length > 0);
}