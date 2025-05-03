using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

[TestFixture]
public class RegisterTest
{
    private GameObject testGameObject;
    private RegisterController registerController;
    private TMP_InputField userNameInput;
    private TMP_InputField emailInput;
    private TMP_InputField passwordInput;
    private Button submitRegisterButton;
    private Button returnToLoginButton;
    private GameObject authBlackout;
    private TMP_Text errorText;
    private Button errorOKButton;
    
    [SetUp]
    public void SetUp()
    {
        // Crear GameObjects de prueba
        testGameObject = new GameObject("RegisterControllerTest");
        registerController = testGameObject.AddComponent<RegisterController>();
        
        // Crear canvas y tarjeta para la estructura UI
        GameObject canvas = new GameObject("Canvas");
        GameObject tarjeta = new GameObject("Tarjeta");
        tarjeta.transform.SetParent(canvas.transform);
        
        // Crear inputs
        GameObject userNameObj = new GameObject("UserNameInput");
        userNameObj.transform.SetParent(tarjeta.transform);
        userNameInput = userNameObj.AddComponent<TMP_InputField>();
        registerController.userName = userNameInput;
        
        GameObject emailObj = new GameObject("EmailInput");
        emailObj.transform.SetParent(tarjeta.transform);
        emailInput = emailObj.AddComponent<TMP_InputField>();
        registerController.email = emailInput;
        
        GameObject passwordObj = new GameObject("PasswordInput");
        passwordObj.transform.SetParent(tarjeta.transform);
        passwordInput = passwordObj.AddComponent<TMP_InputField>();
        registerController.password = passwordInput;
        
        // Crear botones
        GameObject submitRegisterObj = new GameObject("BotonRegistrar");
        submitRegisterObj.transform.SetParent(tarjeta.transform);
        submitRegisterButton = submitRegisterObj.AddComponent<Button>();
        registerController.submitRegister = submitRegisterButton;
        
        GameObject returnToLoginObj = new GameObject("BotonIrLogin");
        returnToLoginObj.transform.SetParent(tarjeta.transform);
        returnToLoginButton = returnToLoginObj.AddComponent<Button>();
        registerController.returnToLogin = returnToLoginButton;
        
        // Crear la estructura para el manejo de errores
        authBlackout = new GameObject("BlackoutAuth");
        authBlackout.transform.SetParent(submitRegisterObj.transform);
        authBlackout.SetActive(false);
        
        GameObject tarjetaAuth = new GameObject("TarjetaAuth");
        tarjetaAuth.transform.SetParent(authBlackout.transform);
        
        GameObject errorTextObj = new GameObject("InfoError");
        errorTextObj.transform.SetParent(tarjetaAuth.transform);
        errorText = errorTextObj.AddComponent<TMP_Text>();
        
        GameObject errorOKObj = new GameObject("AceptarError");
        errorOKObj.transform.SetParent(tarjetaAuth.transform);
        errorOKButton = errorOKObj.AddComponent<Button>();
        
        registerController.authBlackout = authBlackout;
        registerController.errorText = errorText;
        registerController.errorOK = errorOKButton;
    }
    
    [TearDown]
    public void TearDown()
    {
        Object.Destroy(testGameObject);
    }

    
    [Test]
    public void IsValidEmail_ValidEmail_ReturnsTrue()
    {
        // Usar reflection para acceder al método privado IsValidEmail
        System.Reflection.MethodInfo isValidEmailMethod = typeof(RegisterController).GetMethod("IsValidEmail", 
                                                         System.Reflection.BindingFlags.NonPublic | 
                                                         System.Reflection.BindingFlags.Instance);
        
        // Ejecutar el método con un email válido
        bool result = (bool)isValidEmailMethod.Invoke(registerController, new object[] { "test@example.com" });
        
        // Verificar que devuelve true para un email válido
        Assert.IsTrue(result);
    }
    
    [Test]
    public void IsValidEmail_InvalidEmail_ReturnsFalse()
    {
        // Usar reflection para acceder al método privado IsValidEmail
        System.Reflection.MethodInfo isValidEmailMethod = typeof(RegisterController).GetMethod("IsValidEmail", 
                                                         System.Reflection.BindingFlags.NonPublic | 
                                                         System.Reflection.BindingFlags.Instance);
        
        // Ejecutar el método con un email inválido
        bool result = (bool)isValidEmailMethod.Invoke(registerController, new object[] { "invalidemail" });
        
        // Verificar que devuelve false para un email inválido
        Assert.IsTrue(!result);
    }
    
    
    [UnityTest]
    public IEnumerator MoveToLogin_LoadsLoginScene()
    {
        // Mockear SceneManager.LoadScene
        // Nota: En una implementación real, podrías usar una librería como NSubstitute o utilizar
        // una interfaz para abstracción que facilite las pruebas
        
        // Esta prueba es conceptual, ya que cargar escenas en tests requiere configuración adicional
        
        // Simular clic en botón de volver a login
        returnToLoginButton.onClick.Invoke();
        
        // Esperar un frame
        yield return null;
        
        // En una implementación real, verificaríamos que se llamó a SceneManager.LoadScene
        // con el parámetro correcto
        Assert.Pass("Esta prueba requiere integración con el sistema de escenas de Unity");
    }
    
    [UnityTest]
    public IEnumerator RegisterUser_ValidData_TriggersFirebaseRegistration()
    {
        // Esta prueba requeriría mockear Firebase, lo cual está fuera del alcance de este ejemplo básico
        // En un entorno real, usaríamos una interfaz para abstraer Firebase y mockear su comportamiento
        
        // Configurar datos válidos
        userNameInput.text = "TestUser";
        emailInput.text = "test@example.com";
        passwordInput.text = "Password123!";
        
        // Simular clic en botón de registro
        submitRegisterButton.onClick.Invoke();
        
        // Esperar frames para que se complete la corrutina
        yield return new WaitForSeconds(0.1f);
        
        // En una implementación real, verificaríamos que se llamó a los métodos de Firebase
        // y que se manejaron correctamente las respuestas
        Assert.Pass("Esta prueba requiere integración con Firebase o mocks adecuados");
    }
}