using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileController: MonoBehaviour
{
    public Button user;
    private string username = "   Username777";
    public Button email;
    private string userEmail = "   alguien@example.com";
    public Button password;
    private string userPassword = "   ¡Es un secreto!";
    public Button privacy, logout, confirmLogout, denyLogout;
    public GameObject logoutBlackout;

    private FirebaseAuth auth;
    private FirebaseUser activeUser;

    void Start()
    {
        LoadUserInfo();
        user = GameObject.Find("Canvas/BotonUsuario").GetComponent<Button>();
        user.onClick.AddListener(() => ShowText(username, "UsuarioTexto"));
        email = GameObject.Find("Canvas/BotonCorreo").GetComponent<Button>();
        email.onClick.AddListener(() => ShowText(userEmail, "CorreoTexto"));
        password = GameObject.Find("Canvas/BotonContraseña").GetComponent<Button>();
        password.onClick.AddListener(() => ShowText(userPassword, "ContraseñaTexto"));
        privacy = GameObject.Find("Canvas/BotonPolitica").GetComponent<Button>();
        privacy.onClick.AddListener(() => Navigate("PoliticaScene"));
        logout.onClick.AddListener(DeployLogout);
    }

    void ShowText(string newText, string name)
    {
        TMP_Text toChange;
        toChange = GameObject.Find(name).GetComponent<TMP_Text>();
        toChange.text = newText;
    }

    void LoadUserInfo()
    // TO DO
    {
        if (activeUser != null)
        {
            username = "" + activeUser.DisplayName;
            userEmail = "" + activeUser.Email;
        }
    }

    void Navigate(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    void DeployLogout()
    {
        logoutBlackout.SetActive(!logoutBlackout.activeSelf);
        logoutBlackout = GameObject.Find("Canvas/BotonCerrarSesion/BlackoutLogout");
        confirmLogout.onClick.AddListener(LogoutUser);
        denyLogout.onClick.AddListener(() => Navigate("SampleScene"));
    }

    void LogoutUser()
    {
        if (AuthManager.Instance != null)
        {
            AuthManager.Instance.SignOut();
        }
        else if (auth != null)
        {
            auth.SignOut();
        }

        Navigate("SplashScene");
    }

    
}
