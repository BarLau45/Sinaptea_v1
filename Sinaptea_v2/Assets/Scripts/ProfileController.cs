using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileController: MonoBehaviour
{
    public Button user;
    private string username = "   User777";
    public Button email;
    private string userEmail = "   alguien@example.com";
    public Button password;
    private string userPassword = "   contraseña1234";
    public Button privacy;

    void Start()
    {
        user = GameObject.Find("Canvas/BotonUsuario").GetComponent<Button>();
        user.onClick.AddListener(() => ShowText(username, "UsuarioTexto"));
        email = GameObject.Find("Canvas/BotonCorreo").GetComponent<Button>();
        email.onClick.AddListener(() => ShowText(userEmail, "CorreoTexto"));
        password = GameObject.Find("Canvas/BotonContraseña").GetComponent<Button>();
        password.onClick.AddListener(() => ShowText(userPassword, "ContraseñaTexto"));
        privacy = GameObject.Find("Canvas/BotonPolitica").GetComponent<Button>();
        privacy.onClick.AddListener(MoveToPrivacy);
    }

    void ShowText(string newText, string name)
    {
        TMP_Text toChange;
        toChange = GameObject.Find(name).GetComponent<TMP_Text>();
        toChange.text = newText;
    }
    void MoveToPrivacy()
    {
        SceneManager.LoadScene("PoliticaScene");
    }
}
