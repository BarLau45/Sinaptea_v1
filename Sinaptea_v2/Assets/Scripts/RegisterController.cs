using System.Collections;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    public TMP_InputField userName, email, password;
    public Button submitRegister, returnToLogin, errorOK;
    public GameObject authBlackout;
    public TMP_Text errorText;

    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        submitRegister = GameObject.Find("Canvas/Tarjeta/BotonRegistrar").GetComponent<Button>();
        submitRegister.onClick.AddListener(RegisterUser);
        returnToLogin = GameObject.Find("Canvas/Tarjeta/BotonIrLogin").GetComponent<Button>();
        returnToLogin.onClick.AddListener(MoveToLogin);

        if (authBlackout != null)
        {
            errorText = authBlackout.GetComponentInChildren<TMP_Text>();
        }
    }

    void RegisterUser()
    {
        if (string.IsNullOrEmpty(userName.text) || string.IsNullOrEmpty(email.text) || string.IsNullOrEmpty(password.text)) 
        {
            DeployError("Los datos están incompletos");
        }

        if (!IsValidEmail(email.text))
        {
            DeployError("Por favor, ingrese una dirección de correo válida");
        }
        StartCoroutine(RegisterUserAsync());
    }

    IEnumerator RegisterUserAsync()
    {
        // Crear usuario con correo y contraseña en Firebase
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text);
        
        // Esperar a que la tarea termine
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            // Manejar errores de registro
            Debug.LogWarning($"Error al registrar: {registerTask.Exception}");
            FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseException.ErrorCode;
            
            string errorMessage = "Error al registrar usuario";
            
            switch (errorCode)
            {
                case AuthError.EmailAlreadyInUse:
                    errorMessage = "El correo electrónico ya está en uso";
                    break;
                case AuthError.InvalidEmail:
                    errorMessage = "Correo electrónico inválido";
                    break;
                case AuthError.WeakPassword:
                    errorMessage = "La contraseña es demasiado débil";
                    break;
                default:
                    errorMessage = "Error de registro: " + errorCode.ToString();
                    break;
            }
            
            DeployError(errorMessage);
        }
        else
        {
            // Registro exitoso
            Debug.Log("Usuario registrado exitosamente");
            
            // Actualizar el perfil del usuario con el nombre de usuario
            FirebaseUser user = registerTask.Result.User;
            UserProfile profile = new UserProfile
            {
                DisplayName = userName.text
            };
            
            var profileTask = user.UpdateUserProfileAsync(profile);
            yield return new WaitUntil(() => profileTask.IsCompleted);
            
            if (profileTask.Exception != null)
            {
                Debug.LogWarning($"Error al actualizar perfil: {profileTask.Exception}");
                // Podemos continuar aunque falle la actualización del perfil
            }
            
            // Ir a la escena principal o de login
            MoveToLogin();
        }
        
        // Rehabilitar botón de registro
        submitRegister.interactable = true;
    }

    void MoveToLogin()
    {
        SceneManager.LoadScene("LoginScene");
    }

    void DeployError(string text)
    {
        authBlackout.SetActive(!authBlackout.activeSelf);
        authBlackout = GameObject.Find("Canvas/Tarjeta/BotonRegistrar/BlackoutAuth");
        errorText = GameObject.Find("Canvas/Tarjeta/BotonRegistrar/BlackoutAuth/TarjetaAuth/InfoError").GetComponent<TMP_Text>();
        errorText.text = text;
        errorOK = GameObject.Find("Canvas/Tarjeta/BotonRegistrar/BlackoutAuth/TarjetaAuth/AceptarError").GetComponent<Button>();
        errorOK.onClick.AddListener(DeactivateError);

    }

    void DeactivateError()
    {
        authBlackout.SetActive(false);
    }

    bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }
}
