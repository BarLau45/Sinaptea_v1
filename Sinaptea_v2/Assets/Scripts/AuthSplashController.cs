using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthSplashController : MonoBehaviour
{
    public Button register;
    public Button login;

    void Start()
    {
        register = GameObject.Find("Canvas/Tarjeta/BotonRegistro").GetComponent<Button>();
        register.onClick.AddListener(MoveToRegister);
        login = GameObject.Find("Canvas/Tarjeta/BotonIniciarSesion").GetComponent<Button>();
        login.onClick.AddListener(MoveToLogin);
    }

    void MoveToRegister()
    {
        SceneManager.LoadScene("RegistroScene");
    }

    void MoveToLogin()
    {
        SceneManager.LoadScene("LoginScene");
    }

}
