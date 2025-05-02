using System.Collections;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public TMP_InputField email, password;
    public Button submitLogin, returnToRegister, errorOK;
    public GameObject authBlackout;
    public TMP_Text errorText;

    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        submitLogin = GameObject.Find("Canvas/Tarjeta/BotonIngresar").GetComponent<Button>();
        submitLogin.onClick.AddListener(LoginUser);
        returnToRegister = GameObject.Find("Canvas/Tarjeta/BotonIrRegistro").GetComponent<Button>();
        returnToRegister.onClick.AddListener(MoveToRegister);

        if (authBlackout != null)
        {
            errorText = authBlackout.GetComponentInChildren<TMP_Text>();
        }
        
        // Verificar si ya hay una sesión activa
        if (auth.CurrentUser != null)
        {
            // El usuario ya está autenticado, redirigir a la escena principal
            MoveToSample();
        }
    }

    void LoginUser()
    {
        if (string.IsNullOrEmpty(email.text) || string.IsNullOrEmpty(password.text))
        {
            DeployError("Los Datos están incompletos");
        }
        StartCoroutine(LoginUserAsync());
    }

    IEnumerator LoginUserAsync()
    {
        // Iniciar sesión con correo y contraseña en Firebase
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email.text, password.text);
        
        // Esperar a que la tarea termine
        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            // Manejar errores de inicio de sesión
            Debug.LogWarning($"Error al iniciar sesión: {loginTask.Exception}");
            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseException.ErrorCode;
            
            string errorMessage = "Error al iniciar sesión";
            
            switch (errorCode)
            {
                case AuthError.InvalidEmail:
                    errorMessage = "Email inválido";
                    break;
                case AuthError.WrongPassword:
                    errorMessage = "Contraseña incorrecta";
                    break;
                case AuthError.UserNotFound:
                    errorMessage = "Usuario no encontrado";
                    break;
                case AuthError.UserDisabled:
                    errorMessage = "Usuario deshabilitado";
                    break;
                case AuthError.TooManyRequests:
                    errorMessage = "Demasiados intentos. Inténtalo más tarde";
                    break;
                default:
                    errorMessage = "Error de inicio de sesión: " + errorCode.ToString();
                    break;
            }
            
            DeployError(errorMessage);
        }
        else
        {
            // Inicio de sesión exitoso
            FirebaseUser user = loginTask.Result.User;
            Debug.Log($"Inicio de sesión exitoso: {user.Email}");
            
            // Ir a la escena principal
            MoveToSample();
        }
        
        // Rehabilitar botón de inicio de sesión
        submitLogin.interactable = true;
    }

    void MoveToRegister()
    {
        SceneManager.LoadScene("RegistroScene");
    }

    void MoveToSample()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void DeployError(string text)
    {
        authBlackout.SetActive(!authBlackout.activeSelf);
        authBlackout = GameObject.Find("Canvas/Tarjeta/BotonIngresar/BlackoutAuth");
        errorText = GameObject.Find("Canvas/Tarjeta/BotonIngresar/BlackoutAuth/TarjetaAuth/InfoError").GetComponent<TMP_Text>();
        errorText.text = text;
        errorOK = GameObject.Find("Canvas/Tarjeta/BotonIngresar/BlackoutAuth/TarjetaAuth/AceptarError").GetComponent<Button>();
        errorOK.onClick.AddListener(DeactivateError);

    }

    void DeactivateError()
    {
        authBlackout.SetActive(false);
    }
}
